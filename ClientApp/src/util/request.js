
export async function httpGet(url) {
	let res = await fetch(url);

	return respondOrRedirectIfUnauthorized(res);
}

function respondOrRedirectIfUnauthorized(res) {
	if (res.status === 401) {
		window.location.href = '/auth/login';
	}

	return res;
}