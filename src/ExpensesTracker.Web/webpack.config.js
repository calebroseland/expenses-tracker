"use strict";

const path = require("path");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const StatsWriterPlugin = require("webpack-stats-plugin").StatsWriterPlugin;

const extractCSS = new ExtractTextPlugin("[name].[chunkhash].css");
const statsWriter = new StatsWriterPlugin({ filename: "stats.json" });

module.exports = {
    entry: {
        vendors: "./assets/js/vendors.js",
        main: [
            "./assets/css/main.scss",
            "./assets/js/main.js"
        ]
    },
    output: {
        filename: "[name].[chunkhash].js",
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
                test: /\.css$/,
                use: extractCSS.extract(['css-loader', 'postcss-loader'])
            },
            {
                test: /\.less$/i,
                use: extractCSS.extract(['css-loader', 'less-loader'])
            },
            {
                test: /\.scss$/,
                use: extractCSS.extract(['css-loader', 'sass-loader'])
            },
            {
                test: /\.woff2?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
                use: 'url-loader?limit=10000',
            },
            {
                test: /\.(ttf|eot|svg|gif|png)(\?[\s\S]+)?$/,
                use: 'file-loader',
            },
            {
                test: /bootstrap-sass(\\|\/)assets(\\|\/)javascripts(\\|\/)/,
                use: 'imports-loader'
            }
        ]
    },
    plugins: [extractCSS, statsWriter]
};