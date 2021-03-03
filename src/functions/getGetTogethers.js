const AWS = require("aws-sdk");

const dynamodb = new AWS.DynamoDB.DocumentClient();
let tableName = process.env.getTogethersTableName;

module.exports.handler = async () => {
  const count = 8;

  const req = {
    TableName: tableName,
    Limit: count
  };

  const resp = await dynamodb.scan(req).promise();

  const res = {
    statusCode: 200,
    body: JSON.stringify(resp.Items)
  };

  return res;
};