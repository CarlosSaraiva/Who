// ReSharper disable once UnusedParameter
var express = require('express');
var router = express.Router();
var sql = require('mssql');

var config = {
    user: 'Claudio',
    password: 'afc@55216',
    server: 'servagilus.dyndns.org',
    database: 'dbDemo'
}

router.get('/', function (req, res) {
    spwho(function(r) {
        res.json(r);    
    });
});

function spwho(callback) {
    var connection = new sql.Connection(config, function (err) {
        var request = new sql.Request(connection);
// ReSharper disable once DeclarationHides UnusedParameter
// ReSharper disable UnusedParameter
        request.execute('sp_who3', function (err, recordsets, returnValue) {
// ReSharper restore UnusedParameter
            callback(recordsets);
        });
    });
    
    connection.on('error', function (err) {
        console.log(err);
    });
}

module.exports = router;