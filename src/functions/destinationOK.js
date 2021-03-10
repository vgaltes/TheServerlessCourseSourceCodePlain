module.exports.handler = async event => {
    console.log("Destination OK");
    console.log(JSON.stringify(event));

    return "all done";
}