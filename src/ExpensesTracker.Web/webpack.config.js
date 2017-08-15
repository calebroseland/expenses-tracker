"use strict";

const debug = process.env.NODE_ENV !== "production";

const path = require("path");
const webpack = require("webpack");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const StatsWriterPlugin = require("webpack-stats-plugin").StatsWriterPlugin;
const CommonsChunkPlugin = require("webpack/lib/optimize/CommonsChunkPlugin");

const extractCSS = new ExtractTextPlugin(debug ? "[name].css" : "[name].[contenthash].css");
const statsWriter = new StatsWriterPlugin({ filename: "stats.json" });
const commons = new CommonsChunkPlugin({
    name: "main",
    chunks: ["dashboard", "about"]
});

const prodPlugins = debug ? [] : [
    new webpack.optimize.UglifyJsPlugin(),
    new webpack.optimize.OccurrenceOrderPlugin()
];

module.exports = {
    entry: {
        vendors: "./assets/js/vendors.js",
        dashboard: ['./assets/js/pages/dashboard.js'],
        about: ['./assets/js/pages/about.js'],
        designer: ['./assets/designer/index.js'],
        main: [
            "./assets/css/main.scss",
            "./assets/js/main.js"
        ]
    },
    output: {
        filename: debug ? "[name].js" : "[name].[chunkhash].js",
        path: path.resolve(__dirname, "assets", "dist")
    },
    devServer: {
        contentBase: ".",
        host: "localhost",
        port: 9000
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /(node_modules)/,
                use: {
                    loader: 'babel-loader'
                }
            },
            {
                test: /\.vue$/,
                loader: 'vue-loader',
                // options required to work with autoprefixer
                options: {
                    loaders: {
                        'css': 'vue-style-loader!css-loader?autoprefixer',
                        'sass': 'vue-style-loader!css-loader?autoprefixer!sass-loader?indentedSyntax',
                        'scss': 'vue-style-loader!css-loader?autoprefixer!sass-loader'
                    },
                    postcss: [
                      require('autoprefixer')({
                          browsers: ['last 3 versions', 'ie > 8'] // Vue does not support ie 8 and below
                      })
                    ]
                }
            },
            {
                test: /\.css$/,
                use: ExtractTextPlugin.extract({
                    fallback: 'style-loader',
                    use: ['css-loader', 'postcss-loader']
                })
            },
            {
                test: /\.less$/,
                use: ExtractTextPlugin.extract({
                    fallback: 'style-loader',
                    use: ['css-loader', 'less-loader']
                })
            },
            {
                test: /\.scss$/,
                use: ExtractTextPlugin.extract({
                    fallback: 'style-loader',
                    use: ['css-loader', 'sass-loader']
                })
            },
            {
                test: /\.woff2?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
                use: [{
                    loader: 'url-loader',
                    options: { limit: 1000 }
                }]
            },
            {
                test: /\.(ttf|eot|svg|gif|png)(\?[\s\S]+)?$/,
                use: [{ loader: 'file-loader' }]
            },
            {
                test: /bootstrap-sass(\\|\/)assets(\\|\/)javascripts(\\|\/)/,
                use: [{ loader: 'imports-loader' }]
            }
        ]
    },
    plugins: [extractCSS, commons, statsWriter, ...prodPlugins]
};