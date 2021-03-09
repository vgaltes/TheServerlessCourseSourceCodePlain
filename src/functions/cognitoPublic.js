module.exports.handler = async event => {
    const res = {
      statusCode: 200,
      headers: {
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Credentials': true,
      },
      body: JSON.stringify(`Hello from a public enpoint`)
    };
  
    return res;
  }