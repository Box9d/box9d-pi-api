var express = require('express')
var router = express.Router()

router.get('/', function (req, res) {
  res.send('<html><head></head><body><h1>Output</h1></body></html>')
})

module.exports = router