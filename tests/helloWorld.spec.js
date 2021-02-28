const handler = require(`../src/functions/helloWorld`);

describe(`When we invoke the GET /helloWorld endpoint`, () => {
    test(`Should return the right greeting`, async () => {
      const event = { pathParameters: { name: "Manolito" } };
      const response = await handler.handler(event);
      response.body = JSON.parse(response.body);
      expect(response.statusCode).toBe(200);
      expect(response.body).toBe("Hello Manolito");
    }); 
});
