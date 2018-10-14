/*
 * @file webpack配置文件(开发环境)
 * @author yuanzhang
 * @date 2017-04-30
 */
const path = require("path");
const webpack = require("webpack");
const OpenBrowserPlugin = require("open-browser-webpack-plugin");
const BundleAnalyzerPlugin = require("webpack-bundle-analyzer")
  .BundleAnalyzerPlugin;
const htmlWebpackPlugin = require("html-webpack-plugin");
const port = 3000;

module.exports = {
 
  devtool: "cheap-module-eval-source-map",
  entry: {
    // bundle: './app/main.jsx',
    bundle: [
      // "react-hot-loader/patch",// 热更新
      path.join(__dirname, "./app/main.jsx")
    ],
    vendor: ["react", "react-dom", "react-router", "redux"]
  },
  output: {
    path: path.join(__dirname, "/build"),
    filename: "[name].js",
    publicPath: "/" // 热更新
  },
  resolve: {
    extensions: [".js", ".jsx"]
  },
  module: {
    loaders: [
      {
        test: /\.(js|jsx)?$/,
        exclude: /(node_modules|static\lib)/,
        loader: "babel", // 'babel-loader' is also a legal name to reference
        query: {
          presets: ["react", "es2015"]
        }
      }
    ],
    rules: [
      {
        test: /\.(js|jsx)$/,
        use: "babel-loader"
      },
      {
        test: /\.(less|css)$/,
        use: [
          "style-loader",
          "css-loader?#sourceMap",
          "postcss-loader",
          "less-loader"
        ]
      },
      // {
      //     test: /\.scss$/,
      //     use: [{
      //         loader: "style-loader" // 将 JS 字符串生成为 style 节点
      //     }, {
      //         loader: "css-loader" // 将 CSS 转化成 CommonJS 模块
      //     }, {
      //         loader: "sass-loader" // 将 Sass 编译成 CSS
      //     }]
      // },
      {
        test: /\.(png|gif|jpg|jpeg|bmp|svg|eot|woff|ttf)$/i,
        use: "url-loader?limit=5000"
      }
    ]
  },
  plugins: [
    // new webpack.optimize.CommonsChunkPlugin({
    //     name: ['vendor'],
    // }),
    new OpenBrowserPlugin({
      url: `http://localhost:${port}`
    }),
    // new BundleAnalyzerPlugin()
    new htmlWebpackPlugin({
      favicon: path.resolve(__dirname, "./logo.ico"),
      title: "HCI",
      hash: true,
      cache: true,
      template: "./app/index.html"
    })
  ],
  devServer: {
    compress: false, // 启用gzip压缩
    contentBase: path.join(__dirname, "app"),
    port: port, // 运行端口3000
    inline: true,
    hot: true,
    host: "0.0.0.0",
    historyApiFallback: true,
    // 热更新
    overlay: {
      errors: true
    }
    // publicPath:'/public',
    // historyApiFallback:{
    // 	index:'/public/index.html'
    // }
  }
};
