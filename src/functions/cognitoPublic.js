module.exports.handler = async event => {
    const res = {
      statusCode: 200,
      body: JSON.stringify(`Hello from a public enpoint`)
    };
  
    return res;
  }