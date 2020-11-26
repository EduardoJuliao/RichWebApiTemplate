const jwt = require('jsonwebtoken');
const fs = require('fs');

let appsettings = fs.readFileSync('./appsettings.json');
let securitySettings = JSON.parse(appsettings)['SecuritySettings'];

const signInOptions = {
    algorithm: 'HS256',
    issuer: securitySettings.Issuer,
    audience: securitySettings.Audiences ? securitySettings.Audiences : [],
    notBefore: Date.now()
}

const token = jwt.sign({}, securitySettings.Secret, signInOptions);

console.log(token)