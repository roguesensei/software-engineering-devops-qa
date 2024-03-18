import { httpGet, httpPost } from '../util/request';

export async function loadEnrolments() {
	let res = await httpGet('/api/enrolment/get');
	if (res.ok) {
		return await res.json();
	}
	return [];
}

export async function addEnrolment(courseId, userId, courseDate) {
	let res = await httpPost('/api/enrolment/add', { courseId, userId, courseDate });

	return res.ok;
}

export async function editEnrolment(enrolmentId, courseId, userId, courseDate) {
	let res = await httpPost('/api/enrolment/update', { enrolmentId, courseId, userId, courseDate });

	return res.ok;
}

export async function deleteEnrolment(enrolmentId) {
	let res = await httpPost('/api/enrolment/delete', enrolmentId);

	return res.ok;
}