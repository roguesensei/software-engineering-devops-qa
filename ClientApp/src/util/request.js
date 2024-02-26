export async function httpGet(url) {
	let res = await fetch(url);

	return respondOrRedirectIfUnauthorized(res);
}

export async function httpPost(url, body = {}) {}

function respondOrRedirectIfUnauthorized(res) {
	if (res.status === 401) {
		window.location.href = '/login';
	}

	return res;
}