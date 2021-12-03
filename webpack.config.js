// Note this only includes basic configuration for development mode.
// For a more comprehensive configuration check:
// https://github.com/fable-compiler/webpack-config-template

var path = require("path");

let config = require('./sp-fableconfig.json');
let entryFile = config.entry;
let outFile = config.outfile;

module.exports = {
    mode: "production",
    entry: entryFile,
    output: {
        filename: outFile,
        libraryTarget: "commonjs",
    },
    optimization: {
        minimize: false
    },
    module: { 
        rules: [{
            test: /.*skyrimPlatform.declare$/,
            use: path.resolve('loaders/skyrimPlatformLoader.js'),
        }]
    }
}