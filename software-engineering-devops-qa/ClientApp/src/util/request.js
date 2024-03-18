export async function httpGet(url) {
	let res = await fetch(url, {
		headers: getHeaders()
	});

	return respondOrRedirectIfUnauthorized(res);
}

export async function httpPost(url, body = {}) {
	let res = await fetch(url, {
		method: 'POST',
		headers: getHeaders(),
		body: JSON.stringify(body)
	});

	return respondOrRedirectIfUnauthorized(res);
}

function respondOrRedirectIfUnauthorized(res) {
	if (res.status === 401) {
		window.location.href = '/login';
	}

	return res;
}

function getHeaders() {
	let bearer = sessionStorage.getItem('jwt');

	return {
		'Authorization': `Bearer ${bearer}`,
		'content-type': 'application/json'
	}
}