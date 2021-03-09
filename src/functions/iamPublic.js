const http = require("axios");
const aws4 = require("aws4");
const URL = require("url");

const privateApi = process.env.privateApi;

const getDataFromPrivateApi = async () => {
  const url = new URL.URL(privateApi);

  const opts = {    
    host: url.hostname,    
    path: url.pathname,  
  };

  aws4.sign(opts);

  const httpReq = http.get(privateApi, {
    headers: opts.headers,  });

  return (await httpReq).data;
};

module.exports.handler = async event => {
    const privateData = await getDataFromPrivateApi();

    const res = {
      statusCode: 200,
      body: JSON.stringify(`Hello from an IAM public endpoint. The data from the private endpoint is ${privateData}`)
    };
  
    return res;
}