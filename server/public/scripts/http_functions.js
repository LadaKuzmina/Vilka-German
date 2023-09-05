async function httpGet(url)
{
    let response = await fetch(url);
    while (response.status !== 200 && response.status !== 204) {
        response = await fetch(url);
    }

    return response.json();
}

async function httpPost(url, json)
{
    let response = await fetch(url, {
        method: 'POST',
        headers: {
            "Accept": "text/plain",
            "Content-Type": "application/json"
        },
        body: json});

    while (response.status !== 200 && response.status !== 204) {
        response = await fetch(url, {
            method: 'POST',
            headers: {
                "Accept": "text/plain",
                "Content-Type": "application/json"
            },
            body: json});
    }

    return response.json();
}