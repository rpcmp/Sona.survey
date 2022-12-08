export function get<T>(uri: string): Promise<T> {
    return fetch(uri).then(resp => handlerResponse<T>(resp));
}

export function remove<T>(uri: string): Promise<T> {
    return fetch(uri, { method: 'DELETE' }).then(x => handlerResponse<T>(x));
}

export function post<T, TOut>(uri: string, body: T): Promise<TOut> {
    return fetch(uri, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: body ? JSON.stringify(body) : null
    }).then(x => handlerResponse<TOut>(x));
}

export function put<T, TOut>(uri: string, body: T): Promise<TOut> {
    return fetch(uri, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: body ? JSON.stringify(body) : null
    }).then(x => handlerResponse<TOut>(x));
}

function handlerResponse<T>(response: Response): Promise<T> {
    if (response.status == 200) {
        return response.json();
    }

    if (response.status == 204) {
        return Promise.resolve() as Promise<T>;
    }

    return response.text().then(x => {
        alert(x);
        return Promise.reject();
    });
}