"use strict";

const path = require("path");
const webpack = require("webpack");
const ExtractTextPlugin = require("extract-text-webpack-plugin");

const extractCSS = new ExtractTextPlugin("[name].css");

module.exports = {
    entry: {
        bootstrap: "bootstrap-loader/extractStyles",
        main: [
            "./assets/css/main.scss",
            "./assets/js/main.js"
        ]
    },
    output: {
        filename: "[name].js",
        path: path.resolve(__dirname, "assets", "dist")
    },
    devServer: {
        contentBase: ".",
        host: "localhost",
        port: 9000
    },
    module: {
        loaders: [
            {
                test: /\.scss$/,
                use: extractCSS.extract(['css-loader', 'sass-loader'])
            },
            {
                test: /\.woff2?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
                use: 'url-loader?limit=10000',
            },
            {
                test: /\.(ttf|eot|svg)(\?[\s\S]+)?$/,
                use: 'file-loader',
            },
            {
                test: /bootstrap-sass(\\|\/)assets(\\|\/)javascripts(\\|\/)/,
                use: 'imports-loader?jQuery=jquery'
            }
        ]
    },
    plugins: [ extractCSS ]
};