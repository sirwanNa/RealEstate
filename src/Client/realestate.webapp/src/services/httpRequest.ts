type HttpRequestType = "GET" | "POST" | "PUT" | "DELETE";

const HttpRequestTypeValues = {
  GET: "GET" as HttpRequestType,
  POST: "POST" as HttpRequestType,
  PUT: "PUT" as HttpRequestType,
  DELETE: "DELETE" as HttpRequestType,
};

export class HttpRequest {
  // Overloads
  async sendRequest<R>(url: string, method: HttpRequestType): Promise<[R, Response]>;
  async sendRequest<T, R>(url: string, method: HttpRequestType, body: T): Promise<[R, Response]>;

  async sendRequest<T, R>(
    url: string,
    method: HttpRequestType,
    body?: T
  ): Promise<[R, Response]> {   
    const res: Response = await fetch(url, {
      method,
      headers: {
        "Content-Type": "application/json",
      },
      body: body ? JSON.stringify(body) : undefined,
    });

    if (!res.ok) {
      throw new Error(`HTTP error! status: ${res.status}`);
    }

    const result = (await res.json()) as R;
    return [result, res];
  }

  async get<R>(url: string): Promise<[R, Response]> {
    return await this.sendRequest<R>(url, HttpRequestTypeValues.GET);
  }

  async post<T, R>(url: string, body: T): Promise<[R, Response]> {
    return await this.sendRequest<T, R>(url, HttpRequestTypeValues.POST, body);
  }

  async put<T, R>(url: string, body: T): Promise<[R, Response]> {
    return await this.sendRequest<T, R>(url, HttpRequestTypeValues.PUT, body);
  }

  async delete<R>(url: string): Promise<[R, Response]> {
    return await this.sendRequest<R>(url, HttpRequestTypeValues.DELETE);
  }
}
