import { useState } from 'react'
import { auth, register } from '../store/auth';
import { Alert, Button, Card, Divider, TextField } from '@mui/material';
import { useNavigate } from 'react-router';

export default function Login() {
	const [form, setForm] = useState({
		username: '',
		password: '',
		clientId: 'lms-client'
	});
	const [error, setError] = useState();
	const navigate = useNavigate();

	const handleChange = (newForm) => {
		setForm(newForm);
		setError('');
	}

	return (
		<div style={{ flexGrow: 1, display: 'flex', flexDirection: 'column', background: '#313869', height: '100vh' }}>
			<div style={{ marginLeft: '25vw', marginTop: '25vh' }}>
				<form onSubmit={async (e) => {
					e.preventDefault();
					// sessionStorage.clear();

					let res = await auth(form);
					if (res.ok) {
						let tok = await res.text();
						sessionStorage.setItem('jwt', tok);
						console.debug(tok);
						navigate('/');
					}
					else {
						let err = await res.text();
						setError(err);
					}

				}}>
					<Card elevation={3} sx={(theme) => ({ width: '400px', display: 'flex', flexDirection: 'column', alignItems: 'center', padding: theme.spacing(2) })}>
						<TextField
							autoFocus
							fullWidth
							sx={(theme) => ({ marginTop: theme.spacing(1) })}
							type={'text'}
							variant={'outlined'}
							value={form.username}
							onChange={(event) => handleChange({
								...form,
								username: event.target.value
							})}
							label={'Username'}
						/>
						<TextField
							fullWidth
							sx={(theme) => ({ marginTop: theme.spacing(1) })}
							type={'password'}
							variant={'outlined'}
							value={form.password}
							onChange={(event) => handleChange({
								...form,
								password: event.target.value
							})}
							label={'Password'}
						/>
						{error ? <Alert sx={(theme) => ({ marginTop: theme.spacing(1) })} severity='error'>{error}</Alert> : null}
						<Divider sx={(theme) => ({ width: '80%', marginTop: theme.spacing(1) })} />
						<Button sx={(theme) => ({ marginTop: theme.spacing(2), width: '100%' })} type='submit' variant='contained'>Login</Button>
						<Button
							sx={(theme) => ({ marginTop: theme.spacing(2), width: '100%' })}
							onClick={() => {
								sessionStorage.clear();
								(async () => {
									let res = await register(form);
									if (res.ok) {
										let bod = await res.json();

										sessionStorage.setItem('jwt', bod['token']);
										navigate('/');
									}
									else {
										let err = await res.json();
										setError(err['error']);
									}
								})();

							}}
						>
							Register
						</Button>
					</Card>
				</form>
			</div>
		</div>
	)
}