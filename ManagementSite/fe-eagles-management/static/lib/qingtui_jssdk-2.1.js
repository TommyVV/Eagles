!function(bWindow, qtjs) {
    if ("function" == typeof define && (define.amd || define.cmd)) {
        define(function() {
            return qtjs(bWindow)
        })
    } else if (typeof exports === 'object') { //commonjs规范
        module.exports = qtjs(bWindow)
    } else {
        qtjs(bWindow, true)
    }
}(window, function(bWindow, qtjs) {
    var QingtuiJSBridge;
    var initQingtuiJSBridge = function() {
        var ios_obj;
        try {
            ios_obj = typeof bWindow.webkit.messageHandlers.QingtuiJSBridge;
        } catch (e) {
            ios_obj = "undefined";
        }
        if (bWindow.QingtuiJSBridge) {
            QingtuiJSBridge = bWindow.QingtuiJSBridge;
            if (typeof QingtuiJSBridge.invoke == "undefined") {
                QingtuiJSBridge._list = {};
                QingtuiJSBridge.invoke = function(n, p, f) {
                    var t = Math.random().toString(36).replace('0.', 'qt');
                    this._list[t] = f;
                    this._invoke(JSON.stringify({
                        funName: n,
                        params: p,
                        tag: t
                    }));

                };
                QingtuiJSBridge._result = function(n, r, t, canReplay) {
                    canReplay = canReplay || false;
                    var res = JSON.parse(r);
                    this._list[t](res);
                    if (!canReplay) {
                        delete this._list[t];
                    }

                }
            }
        } else if (ios_obj != "undefined") {
            QingtuiJSBridge = bWindow.QingtuiJSBridge = bWindow.webkit.messageHandlers.QingtuiJSBridge;
            if (typeof QingtuiJSBridge.invoke == "undefined") {
                QingtuiJSBridge._list = {};
                QingtuiJSBridge.invoke = function(n, p, f) {
                    var t = Math.random().toString(36).replace('0.', 'qt');
                    this._list[t] = f;
                    this.postMessage(JSON.stringify({
                        funName: n,
                        params: p,
                        tag: t
                    }))
                };
                QingtuiJSBridge._result = function(n, r, t, canReplay) {
                    canReplay = canReplay || false;
                    var res = JSON.parse(r);
                    this._list[t](res);
                    if (!canReplay) {
                        delete this._list[t];
                    }

                }
            }

        }
        return true
    }();

    function runInvoke(localFuncName, localFuncArgs, callback) {
        if (bWindow.QingtuiJSBridge) {
            //TODO:config接口未配置完成时使用
            // if (localFuncName == 'preVerifyJSAPI') {
            //     gloabComplete(localFuncName, {
            //         errMsg: 'preVerifyJSAPI:fuck'
            //     }, callback)
            // }
            QingtuiJSBridge.invoke(localFuncName, formartArgs(localFuncArgs), function(re) {
                gloabComplete(localFuncName, re, callback)
            })

        } else {
            debugInfo(localFuncName, callback)
        }
    }

    function formartArgs(localFuncArgs) {
        localFuncArgs = localFuncArgs || {};
        localFuncArgs.appId = QTconfig.appId;
        localFuncArgs.verifyAppId = QTconfig.appId;
        localFuncArgs.verifySignType = "sha1";
        localFuncArgs.verifyTimestamp = QTconfig.timestamp + "";
        localFuncArgs.verifyNonceStr = QTconfig.nonceStr;
        localFuncArgs.verifySignature = QTconfig.signature;
        return localFuncArgs
    }

    function gloabComplete(localFuncName, re, callback) {
        // re = JSON.parse(re);
        console.log(localFuncName, callback);
        var errorMessage, indexNo, status;
        delete re.err_code;
        delete re.err_desc;
        delete re.err_detail;
        errorMessage = re.errMsg;
        if (!errorMessage) {
            errorMessage = re.err_msg || 'failed_client_error';
            delete re.err_msg;
            errorMessage = h(localFuncName, errorMessage);
            re.errMsg = errorMessage;
        }
        callback = callback || {};
        if (callback._complete) {
            callback._complete(re);
            delete callback._complete
        }
        errorMessage = re.errMsg || "";
        if (QTconfig.debug && !callback.isInnerInvoke) {
            alert(JSON.stringify(re))
        }
        indexNo = errorMessage.indexOf(":")
        status = errorMessage.substring(indexNo + 1)
        switch (status) {
            case "ok":
                if (callback.success) {
                    callback.success(re);
                }
                break;
            case "cancel":
                if (callback.cancel) {
                    callback.cancel(re);
                }
                break;
            default:
                if (callback.fail) {
                    callback.fail(re);
                }
        }
        if (callback.complete) {
            callback.complete(re);
        }
    }

    function h(a, b) {
        var e;
        var f;
        var c = a;
        var d = p[c];
        if (d) {
            c = d
        }
        e = b.substring(f + 1);
        if ("confirm" == e) {
            e = "ok"
        }
        if ("failed" == e) {
            e = "fail"
        }
        if (-1 != e.indexOf("failed_")) {
            e = e.substring(7)
        }
        if (-1 != e.indexOf("fail_")) {
            e = e.substring(5)
        }
        e = e.replace(/_/g, " ");
        e = e.toLowerCase();
        if ("access denied" == e || "no permission to execute" == e) {
            e = "permission denied"
        }
        if ("config" == c && "function not exist" == e) {
            e = "ok"
        }
        if ("" == e) {
            e = "fail"
        }
        return b = c + ":" + e
    }

    function translateApiName(apiNameList) {
        var i, sdkName, translateApiName;
        if (apiNameList) {
            for (i = 0; i < apiNameList.length; i++) {
                sdkName = apiNameList[i];
                translateApiName = sdkNameToApiName[sdkName];
                if (translateApiName) {
                    apiNameList[i] = translateApiName
                };
            }
            return apiNameList
        }
    }

    function debugInfo(apiName, args) {
        if (!(!QTconfig.debug || args && args.isInnerInvoke)) {
            var c = apiNameToSdkName[apiName];
            if (c) {
                apiName = c
            }
            if (args && args._complete) {
                delete args._complete
            }
            console.log('"' + apiName + '",', args || "")
        }
    }

    function k() {
        if (D.preVerifyState != 0) {
            if (!(notMobile || isDebugger || QTconfig.debug || "420" > clientVersion || D.systemType < 0 || A)) {
                A = !0
                D.appId = QTconfig.appId;
                D.initTime = C.initEndTime - C.initStartTime;
                D.preVerifyTime = C.preVerifyEndTime - C.preVerifyStartTime;
                H.getNetworkType({
                    isInnerInvoke: !0,
                    success: function(a) {
                        var b, c;
                        D.networkType = a.networkType;
                        b = "http://open.weixin.qq.com/sdk/report?v=" + D.version + "&o=" + D.preVerifyState + "&s=" + D.systemType + "&c=" + D.clientVersion + "&a=" + D.appId + "&n=" + D.networkType + "&i=" + D.initTime + "&p=" + D.preVerifyTime + "&u=" + D.url;
                        c = new Image;
                        // c.src = b
                        console.log(b);
                    }
                });
            }
        }
    }

    function getNowTimestamp() {
        return (new Date).getTime()
    }

    function isReady(b) {
        if (isQTClient) {
            if (bWindow.QingtuiJSBridge) {
                b()
            } else {
                if (documentRoot.addEventListener) {
                    documentRoot.addEventListener("QingtuiJSBridgeReady", b, false)
                }
            }
        }

    }

    function n() {
        H.invoke || (H.invoke = function(b, c, d) {
            a.QingtuiJSBridge && QingtuiJSBridge.invoke(b, e(c), d)
        }, H.on = function(b, c) {
            a.QingtuiJSBridge && QingtuiJSBridge.on(b, c)
        })
    }

    var coordtransform = function() {
        //定义一些常量
        var PI = 3.1415926535897932384626;
        var a = 6378245.0;
        var ee = 0.00669342162296594323;
        /**
         * 百度坐标系 (BD-09) 与 火星坐标系 (GCJ-02)的转换
         * 即 百度 转 谷歌、高德
         * @param bd_lon
         * @param bd_lat
         * @returns {*[]}
         */
        var bd09togcj02 = function bd09togcj02(bd_lon, bd_lat) {
            var bd_lon = +bd_lon;
            var bd_lat = +bd_lat;
            var x = bd_lon - 0.0065;
            var y = bd_lat - 0.006;
            var z = Math.sqrt(x * x + y * y) - 0.00002 * Math.sin(y * PI);
            var theta = Math.atan2(y, x) - 0.000003 * Math.cos(x * PI);
            var gg_lng = z * Math.cos(theta);
            var gg_lat = z * Math.sin(theta);
            return [gg_lng, gg_lat]
        };

        /**
         * 火星坐标系 (GCJ-02)的转换 与 百度坐标系 (BD-09)
         * 即谷歌、高德 转 百度
         * @param bd_lon
         * @param bd_lat
         * @returns {*[]}
         */
        var gcj02tobd09 = function gcj02tobd09(gg_lon, gg_lat) {
            var gg_lon = +gg_lon;
            var gg_lat = +gg_lat;
            var x = gg_lon;
            var y = gg_lat;

            var z = Math.sqrt(x * x + y * y) + 0.00002 * Math.sin(y * PI);

            var theta = Math.atan2(y, x) + 0.000003 * Math.cos(x * PI);

            var bd_lon = z * Math.cos(theta) + 0.0065;

            var bd_lat = z * Math.sin(theta) + 0.006;
            return [bd_lon, bd_lat]
        };

        /**
         * GCJ02 转换为 WGS84
         * @param lng
         * @param lat
         * @returns {*[]}
         */
        var gcj02towgs84 = function gcj02towgs84(lng, lat) {
            var lat = +lat;
            var lng = +lng;
            if (out_of_china(lng, lat)) {
                return [lng, lat]
            } else {
                var dlat = transformlat(lng - 105.0, lat - 35.0);
                var dlng = transformlng(lng - 105.0, lat - 35.0);
                var radlat = lat / 180.0 * PI;
                var magic = Math.sin(radlat);
                magic = 1 - ee * magic * magic;
                var sqrtmagic = Math.sqrt(magic);
                dlat = (dlat * 180.0) / ((a * (1 - ee)) / (magic * sqrtmagic) * PI);
                dlng = (dlng * 180.0) / (a / sqrtmagic * Math.cos(radlat) * PI);
                var mglat = lat + dlat;
                var mglng = lng + dlng;
                return [lng * 2 - mglng, lat * 2 - mglat]
            }
        };
        /**
         * WGS84 转换为 GCJ02
         *
         * @param lat
         * @param lng
         * @return
         */
        var wgs84togcj02 = function wgs84togcj02(lng, lat) {
            var lat = +lat;
            var lng = +lng;
            if (out_of_china(lng, lat)) {
                return [lng, lat]
            } else {
                var dLat = transformlat(lng - 105.0, lat - 35.0);
                var dLon = transformlng(lng - 105.0, lat - 35.0);
                var radLat = lat / 180.0 * PI;
                var magic = Math.sin(radLat);
                magic = 1 - ee * magic * magic;
                var sqrtMagic = Math.sqrt(magic);
                dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * PI);
                dLon = (dLon * 180.0) / (a / sqrtMagic * Math.cos(radLat) * PI);
                var mgLat = lat + dLat;
                var mgLon = lng + dLon;
            }
            return [mgLon, mgLat]
        }


        /**
         * 百度坐标系 (BD-09) 转换为 WGS84
         * @param lng
         * @param lat
         * @returns {*[]}
         */
        var bd09towgs84 = function bd09towgs84(bd_lon, bd_lat) {
            var gcj02 = bd09togcj02(bd_lon, bd_lat);
            return gcj02towgs84(gcj02[0], gcj02[1]);
        };
        /**
         * WGS84 转换为 百度坐标系 (BD-09)
         * @param lng
         * @param lat
         * @returns {*[]}
         */
        var wgs84tobd09 = function wgs84tobd09(lng, lat) {
            var gcj02 = wgs84togcj02(lng, lat);
            return gcj02tobd09(gcj02[0], gcj02[1]);
        };

        var transformlat = function transformlat(lng, lat) {
            var lat = +lat;
            var lng = +lng;
            var ret = -100.0 + 2.0 * lng + 3.0 * lat + 0.2 * lat * lat + 0.1 * lng * lat + 0.2 * Math.sqrt(Math.abs(lng));
            ret += (20.0 * Math.sin(6.0 * lng * PI) + 20.0 * Math.sin(2.0 * lng * PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.sin(lat * PI) + 40.0 * Math.sin(lat / 3.0 * PI)) * 2.0 / 3.0;
            ret += (160.0 * Math.sin(lat / 12.0 * PI) + 320 * Math.sin(lat * PI / 30.0)) * 2.0 / 3.0;
            return ret
        };

        var transformlng = function transformlng(lng, lat) {
            var lat = +lat;
            var lng = +lng;
            var ret = 300.0 + lng + 2.0 * lat + 0.1 * lng * lng + 0.1 * lng * lat + 0.1 * Math.sqrt(Math.abs(lng));
            ret += (20.0 * Math.sin(6.0 * lng * PI) + 20.0 * Math.sin(2.0 * lng * PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.sin(lng * PI) + 40.0 * Math.sin(lng / 3.0 * PI)) * 2.0 / 3.0;
            ret += (150.0 * Math.sin(lng / 12.0 * PI) + 300.0 * Math.sin(lng / 30.0 * PI)) * 2.0 / 3.0;
            return ret
        };

        /**
         * 判断是否在国内，不在国内则不做偏移
         * @param lng
         * @param lat
         * @returns {boolean}
         */
        var out_of_china = function out_of_china(lng, lat) {
            var lat = +lat;
            var lng = +lng;
            // 纬度3.86~53.55,经度73.66~135.05
            return !(lng > 73.66 && lng < 135.05 && lat > 3.86 && lat < 53.55);
        };

        return {
            bd09togcj02: bd09togcj02,
            gcj02tobd09: gcj02tobd09,
            gcj02towgs84: gcj02towgs84,
            bd09towgs84: bd09towgs84,
            wgs84togcj02: wgs84togcj02,
            wgs84tobd09: wgs84tobd09
        }
    }()

    var sdkNameToApiName, apiNameToSdkName, documentRoot, docTitle, ua, platform, notMobile, isDebugger, isQTClient, QTconfig,
        isAndroid, isIos, clientVersion, A, B, C, D, E, F, G, H, LocationTool;
    console.log(bWindow, qtjs);
    if (!bWindow.jQingTui) {
        sdkNameToApiName = {
            checkJsApi: "config",
            onMenuShareTimeline: "menu:share:timeline",
            onMenuShareAppMessage: "menu:share:appmessage",
            onMenuShareQQ: "menu:share:qq",
            onMenuShareWeibo: "menu:share:weiboApp",
            onMenuShareQZone: "menu:share:QZone",
            previewImage: "previewImage",
            getLocation: "getLocation",
            startGeoLocation: "startGeoLocation",
            stopGeoLocation: "stopGeoLocation",
            openProductSpecificView: "openProductViewWithPid",
            addCard: "batchAddCard",
            openCard: "batchViewCard",
            chooseWXPay: "getBrandWCPayRequest"
        };
        apiNameToSdkName = function() {
            var key;
            var apiNameToSdkName = {};
            for (key in sdkNameToApiName) {
                apiNameToSdkName[sdkNameToApiName[key]] = key;
            }
            return apiNameToSdkName
        }();
        documentRoot = bWindow.document;
        // docTitle = documentRoot.title;
        ua = navigator.userAgent.toLowerCase();
        platform = navigator.platform.toLowerCase();
        notMobile = !(!platform.match("mac") && !platform.match("win"));
        isDebugger = -1 != ua.indexOf("qtdebugger");
        isQTClient = -1 != ua.indexOf("qingtui");
        isAndroid = -1 != ua.indexOf("android");
        isIos = -1 != ua.indexOf("iphone") || -1 != ua.indexOf("ipad");
        clientVersion = function() {
            var a = ua.match(/qingtui\/(\d+\.\d+\.\d+)/) || ua.match(/qingtui\/(\d+)/);
            return a ? a[1] : ""
        }();
        A = !1;
        B = !1;
        C = {
            initStartTime: getNowTimestamp(),
            initEndTime: 0,
            preVerifyStartTime: 0,
            preVerifyEndTime: 0
        };
        D = {
            version: 1,
            appId: "",
            initTime: 0,
            preVerifyTime: 0,
            networkType: "",
            preVerifyState: 1,
            systemType: isIos ? 1 : isAndroid ? 2 : -1,
            clientVersion: clientVersion,
            url: encodeURIComponent(location.href)
        };
        QTconfig = {
            debug: false,
            check: false
        };
        F = {
            _completes: []
        };
        G = {
            state: 0,
            data: {}
        };
        isReady(function() {
            C.initEndTime = getNowTimestamp()
        });
        LocationTool = {
            coverntType: function(position, mType) {
                var yType = position.type || 'wgs84';
                position.longitude = parseFloat(position.longitude);
                position.latitude = parseFloat(position.latitude);
                var finalPos = [position.longitude, position.latitude]
                if (D.clientVersion >= 400) {
                    if (typeof position.type == 'undefined' && D.systemType == 2) {
                        yType = 'bd09'
                    }
                }
                if (yType != mType) {
                    if (yType == 'bd09' && mType == "wgs84") {
                        finalPos = coordtransform.bd09towgs84(position.longitude, position.latitude);
                    } else if (yType == 'bd09' && mType == "gcj02") {
                        finalPos = coordtransform.bd09togcj02(position.longitude, position.latitude);
                    } else if (yType == 'gcj02' && mType == "wgs84") {
                        finalPos = coordtransform.gcj02towgs84(position.longitude, position.latitude);
                    } else if (yType == 'gcj02' && mType == "bd09") {
                        finalPos = coordtransform.gcj02tobd09(position.longitude, position.latitude);
                    } else if (yType == 'wgs84' && mType == "bd09") {
                        finalPos = coordtransform.wgs84tobd09(position.longitude, position.latitude);
                    } else if (yType == 'wgs84' && mType == "gcj02") {
                        finalPos = coordtransform.wgs84togcj02(position.longitude, position.latitude);
                    }

                }
                position.longitude = finalPos[0];
                position.latitude = finalPos[1];
                delete position.type
            },
            geoOptionMap: {},
            geoProssStatus: false,
            startGeoOPStatus: false,
            geoProssRes: {
                latitude: 1,
                longitude: 2,
                precision: 300,
                addr: 'sss'
            },
            startGeoPross: function(option) {
                var tiiiiim = new Date().getTime();
                var now;
                runInvoke(sdkNameToApiName.startGeoLocation, option, function() {
                    option.success = function(res) {
                        LocationTool.geoProssRes = res;
                        LocationTool.geoProssStatus = true;
                        now = new Date().getTime();
                        console.log('geo pross is start', now - tiiiiim);
                        if (!LocationTool.startGeoOPStatus) {
                            LocationTool.startGeoOP()
                        }
                        tiiiiim = new Date().getTime();
                    }
                    return option
                }())
            },
            stopGeoPross: function(option) {
                // runInvoke(sdkNameToApiName.stopGeoLocation, {}, {})
                var gom = LocationTool.geoOptionMap
                var gomStr = JSON.stringify(gom)
                if (gomStr != '{}') {
                    for (var id in gom) {
                        if (gom.hasOwnProperty(id)) {
                            LocationTool.removeGeoOption(id)
                        }
                    }
                }
                if (LocationTool.geoProssStatus) {
                    runInvoke(sdkNameToApiName.stopGeoLocation, {}, function() {
                        option._complete = function() {
                            LocationTool.geoProssStatus = false;
                            LocationTool.startGeoOPStatus = false;
                        }
                        return option
                    }())
                    console.log('stopGeoLocation');
                } else {
                    if (option.success) {
                        option.success();
                    }
                    console.log('stopGeoLocation id al');
                }


            },
            startGeoOP: function() {
                var geoOptionMap = LocationTool.geoOptionMap;
                LocationTool.startGeoOPStatus = true
                for (var optionId in geoOptionMap) {
                    if (geoOptionMap.hasOwnProperty(optionId)) {
                        var option = geoOptionMap[optionId];
                        var geoid = option._start(optionId);
                        option.geoId = geoid;
                    }
                }
            },
            addGeoOption: function(option) {
                var optionId = Math.random().toString().replace('0.', 'geo_');
                option._success = function(optionId) {
                    var res = LocationTool.geoProssRes;
                    var data = {
                        latitude: res.latitude,
                        longitude: res.longitude,
                        precision: parseInt(res.precision),
                        type: res.type,
                        id: optionId
                    }
                    if (option.formatAddr) {
                        data.addr = res.addr;
                    }
                    LocationTool.coverntType(data, option.type)
                    option.success(data)
                }
                option._start = function(optionId) {
                    option._success(optionId);
                    var geoid = setInterval(function() {
                        option._success(optionId)
                    }, option.interval);
                    return geoid
                }
                option.id = optionId;
                LocationTool.geoOptionMap[optionId] = option;
                if (LocationTool.startGeoOPStatus) {
                    var geoid = option._success(optionId)
                    option.geoId = geoid;
                }
            },
            removeGeoOption: function(optionid) {
                var geoOP = LocationTool.geoOptionMap[optionid];
                if (geoOP.geoId) {
                    clearInterval(geoOP.geoId)
                    delete LocationTool.geoOptionMap[optionid];
                }


            }
        }
        H = {
            config: function(config) {
                QTconfig = config, debugInfo("config", config);
                var isCheck = QTconfig.check === false ? false : true;
                isReady(function() {
                    var a, d, e;
                    if (isCheck) {
                        C.preVerifyStartTime = getNowTimestamp()
                        runInvoke(sdkNameToApiName.checkJsApi, {
                            verifyJsApiList: translateApiName(QTconfig.jsApiList)
                        }, function() {
                            F._complete = function(a) {
                                C.preVerifyEndTime = getNowTimestamp(), G.state = 1, G.data = a
                            }, F.success = function() {
                                D.preVerifyState = 0
                            }, F.fail = function(a) {
                                F._fail ? F._fail(a) : G.state = -1
                            };
                            var a = F._completes;
                            a.push(function() {
                                k()
                            });
                            F.complete = function() {
                                for (var c = 0, d = a.length; d > c; ++c) a[c]();
                                F._completes = []
                            };
                            return F
                        }())
                        C.preVerifyStartTime = getNowTimestamp();
                    } else {
                        G.state = 1;
                        var a = F._completes;
                        var e = a.length;
                        for (var d = 0; e > d; ++d) {
                            a[d]();
                        }
                        F._completes = []
                    }
                });
                if (QTconfig.beta) {
                    n();
                }
            },
            ready: function(a) {
                0 != G.state ? a() : (F._completes.push(a), !isQTClient && QTconfig.debug && a())
            },
            error: function(a) {
                "4.7.0" > clientVersion || B || (B = !0, -1 == G.state ? a(G.data) : F._fail = a)
            },
            // 检查api是否可用
            checkJsApi: function(a) {
                var b = function(a) {
                    var c, d, b = a.checkResult;
                    for (c in b) d = p[c], d && (b[d] = b[c], delete b[c]);
                    return a
                };
                runInvoke("checkJsApi", {
                    jsApiList: i(a.jsApiList)
                }, function() {
                    return a._complete = function(a) {
                        if (x) {
                            var c = a.checkResult;
                            c && (a.checkResult = JSON.parse(c))
                        }
                        a = b(a)
                    }, a
                }())
            },
            // 开启录音
            startRecord: function(a) {
                runInvoke("startRecord", {}, a)
            },
            // 停止录音
            stopRecord: function(a) {
                runInvoke("stopRecord", {}, a)
            },
            // 监听录音自动停止
            onVoiceRecordEnd: function(a) {
                runInvoke("onVoiceRecordEnd", {}, a)
            },
            // 播放录音
            playVoice: function(a) {
                runInvoke("playVoice", { localId: a.localId }, a)
            },
            // 暂停播放录音
            pauseVoice: function(a) {
                runInvoke("pauseVoice", { localId: a.localId }, a)
            },
            // 停止播放录音
            stopVoice: function(a) {
                runInvoke("stopVoice", { localId: a.localId }, a)
            },
            // 监听录音播放停止
            onVoicePlayEnd: function(a) {
                runInvoke("onVoicePlayEnd", {}, a)
            },
            // 上传语音
            uploadVoice: function(a) {
                runInvoke("uploadVoice", { localId: a.localId, isShowProgressTips: 0 == a.isShowProgressTips ? 0 : 1 }, a)
            },
            // 下载语音
            downloadVoice: function(a) {
                runInvoke("downloadVoice", { localId: a.localId, isShowProgressTips: 0 == a.isShowProgressTips ? 0 : 1 }, a)
            },
            // 选择图片
            chooseImage: function(a) {
                runInvoke("chooseImage", {
                    count: a.count || 9,
                    sizeType: a.sizeType || ["original", "compressed"],
                    sourceType: a.sourceType || ["album", "camera"]
                }, function() {
                    return a._complete = function(a) {
                        if (typeof a.localIds === 'string') {
                            var b = a.localIds;
                            b && (a.localIds = JSON.parse(b))
                        }
                    }, a
                }())
            },
            // 预览图片
            previewImage: function(a) {
                runInvoke(sdkNameToApiName.previewImage, {
                    btns: a.btns || ["forward", "download"],
                    current: a.current,
                    urls: a.urls
                }, a)
            },
            //上传图片
            uploadImage: function(a) {
                if (typeof a.localId == 'object') {
                    a.localId = a.localId.join();
                }
                runInvoke("uploadFile", {
                    localId: a.localId,
                    isShowProgressTips: 0 == a.isShowProgressTips ? 0 : 1
                }, a)
            },
            //下载图片
            downloadImage: function(a) {
                if (typeof a.serverId == 'object') {
                    a.serverId = a.serverId.join();
                }
                runInvoke("downloadFile", {
                    serverId: a.serverId,
                    isShowProgressTips: 0 == a.isShowProgressTips ? 0 : 1
                }, a)
            },
            //上传文件
            uploadFile: function(a) {
                if (typeof a.localId == 'object') {
                    a.localId = a.localId.join();
                }
                runInvoke("uploadFile", {
                    localId: a.localId,
                    isShowProgressTips: 0 == a.isShowProgressTips ? 0 : 1
                }, a)
            },
            //下载文件
            downloadFile: function(a) {
                if (typeof a.serverId == 'object') {
                    a.serverId = a.serverId.join();
                }
                runInvoke("downloadFile", {
                    serverId: a.serverId,
                    isShowProgressTips: 0 == a.isShowProgressTips ? 0 : 1
                }, a)
            },
            // 获取网络类型
            getNetworkType: function(a) {
                var b = function(a) {
                    var c, d, e, b = a.errMsg;
                    if (a.errMsg = "getNetworkType:ok", c = a.subtype, delete a.subtype, c) a.networkType = c;
                    else switch (d = b.indexOf(":"), e = b.substring(d + 1)) {
                        case "wifi":
                        case "edge":
                        case "wwan":
                            a.networkType = e;
                            break;
                        default:
                            a.errMsg = "getNetworkType:fail"
                    }
                    return a
                };
                runInvoke("getNetworkType", {}, function() {
                    return a._complete = function(a) {
                        a = b(a)
                    }, a
                }())
                var version = ua.match(/nettype\/(\w+)/);
                return a ? a[1] : ""
                console.log(version);
                if (a.success && version) {
                    a.success({
                        networkType: version[1]
                    })
                }
            },
            // 打开位置
            // openLocation: function (a) {
            //     runInvoke("openLocation", {
            //         latitude: a.latitude,
            //         longitude: a.longitude,
            //         name: a.name || "",
            //         address: a.address || "",
            //         scale: a.scale || 28,
            //         infoUrl: a.infoUrl || ""
            //     }, a)
            // },
            // 获取地理位置
            getLocation: function(option) {
                option = option || {};
                console.log(clientVersion);
                if (option.interval && option.interval > 0) {
                    if (clientVersion < 430) {
                        if (option.fail) {
                            option.fail({
                                errMsg: 'client version not suppout interval'
                            })
                        }
                        return
                    }
                    option.interval = option.interval < 1000 ? 1000 : option.interval;
                    if (LocationTool.geoProssStatus) {
                        LocationTool.addGeoOption(option);
                    } else {
                        var GeoOp = {};
                        if (isIos) {
                            GeoOp.distance = 10
                        }
                        if (isAndroid) {
                            GeoOp.interval = 1000
                        }
                        LocationTool.startGeoPross(GeoOp)
                        LocationTool.addGeoOption(option);
                    }
                } else {
                    runInvoke(sdkNameToApiName.getLocation, {
                        type: option.type || "wgs84",
                        formatAddr: option.formatAddr || false,
                        interval: option.interval || 0
                    }, function() {
                        option._complete = function(a) {
                            console.log(a);
                            LocationTool.coverntType(a, this.type)
                        }
                        return option
                    }())
                }

            },
            //关闭连续定位，id为空则关闭所有的连续定位
            stopLocation: function(option) {
                option = option || {};
                if (option.id) {
                    LocationTool.removeGeoOption(option.id)
                    var gomStr = JSON.stringify(LocationTool.geoOptionMap)
                    if (gomStr == '{}') {
                        LocationTool.stopGeoPross(option);
                    } else {
                        if (option.success) {
                            option.success();
                        }
                    }


                } else {
                    LocationTool.stopGeoPross(option);
                }


            },
            // 关闭窗口
            closeWindow: function(a) {
                a = a || {}, runInvoke("closeWindow", {}, a)
            },
            // 调用扫一扫功能
            scanQRCode: function(a) {
                a = a || {}, runInvoke("scanQRCode", {
                    needResult: a.needResult || 0,
                    //scanType: a.scanType || ["qrCode", "barCode"]
                }, function() {
                    return a._complete = function(a) {
                        var b, c;
                        y = false;
                        y && (b = a.resultStr, b && (c = JSON.parse(b), a.resultStr = c && c.scan_code && c.scan_code.scan_result))
                    }, a
                }())
            },
            // 打开指定会话
            openChat: function(a) {
                a = a || {}, runInvoke("openChat", {
                    chatType: a.chatType || "chat",
                    chatId: a.chatId || ""
                }, a)
            },
            // 打开指定会话
            openSignChat: function(a) {
                a = a || {}, runInvoke("openSingleChatView", {
                    id: a.id || "",
                    scope: a.scope || "open"
                }, a)
            },
            // 打开联系人详情
            openUserDetail: function(a) {
                a = a || {}, runInvoke("openUserDetail", {
                    id: a.id || "",
                    scope: a.scope || "open"
                }, a)
            },
            // 打开联系人详情
            openContacts: function(a) {
                a = a || {}, runInvoke("openContacts", {
                    max: a.max > 300 ? 300 : a.max || 300,
                    multiple: a.multiple || false,
                    selectAll: a.selectAll || true,
                    filterGuest: a.filterGuest || false,
                    scope: a.scope || "open",
                    users: a.users || [],
                    title: a.title || '选择联系人',
                    needSelf: a.needSelf || false,
                    allowClear: a.empty || false
                }, function () {
                    a._complete = function (res) {
                        if (res.data) {
                            var data = res.data;
                            if (data.length != 0 && data[0].avatar.indexOf('qingtui.im') != -1) {
                                return res;
                            }
                            for (var i = 0; i < data.length; i++) {
                                var domain = 'https://avatarcdn.qingtui.im/';
                                data[i].avatar = domain + data[i].avatar;
                            }
                            return res;
                        }
                    }
                    return a
                }())
            },

            // 自定义打开通讯录
            openCustomContacts: function (a) {
                if (a.max > 300) {
                    alert("max不能大于300");
                    return;
                }
                if (a.candidateUsers.length === 0) {
                    alert('candidateUsers列表不能为空');
                    return;
                }
                // 判断不能操作的用户是否在待选用户中
                if (a.disabledUsers.length != 0) {
                    var b = [];
                    for (var i = 0; i < a.disabledUsers.length; i++) {
                        if (a.candidateUsers.indexOf(a.disabledUsers[i]) === -1) {
                            b.push(a.disabledUsers[i]);
                        }
                    }
                    if (b.length != 0) {
                        alert('disabledUsers存在没有包含在candidateUsers中的id:' + b);
                        return;
                    }
                }
                // 判断默认用户是否在待选用户中
                if (a.users.length != 0) {
                    var c = [];
                    for (var i = 0; i < a.users.length; i++) {
                        if (a.candidateUsers.indexOf(a.users[i]) === -1) {
                            c.push(a.users[i]);
                        }
                    }
                    if (c.length != 0) {
                        alert('users存在没有包含在candidateUsers中的id:' + c);
                        return;
                    }
                }
                a = a || {}, runInvoke("openCustomContacts", {
                    max: a.max > 300 ? 300 : a.max || 300,
                    multiple: a.multiple || false,
                    filterGuest: a.filterGuest || false,
                    scope: a.scope || "open",
                    users: a.users.length > 300 ? a.users.slice(0, 300) : a.users || [],
                    title: a.title || '选择联系人',
                    needSelf: a.needSelf || false,
                    candidateUsers: a.candidateUsers || [],
                    disabledUsers: a.disabledUsers.length > 300 ? a.disabledUsers.slice(0, 300) : a.disabledUsers || [],
                    allowClear: a.allowClear || false,
                    theme: a.theme || 0
                }, function () {
                    a._complete = function (res) {
                        if (res.data) {
                            var data = res.data;
                            if (data.length != 0 && data[0].avatar.indexOf('qingtui.im') != -1) {
                                return res;
                            }
                            for (var i = 0; i < data.length; i++) {
                                var domain = 'https://avatarcdn.qingtui.im/';
                                data[i].avatar = domain + data[i].avatar;
                            }
                            return res;
                        }
                    }
                    return a
                }())
            },

            // 获取jsAPI版本
            getJsApiVersion: function(a) {
                a = a || {}, runInvoke("getJsApiVersion", {}, a)
            },

            //选择文件
            chooseFile: function (a) {
                runInvoke("chooseFile", {
                    count: a.count || 9
                }, a)
            },

            // Ajax
            ajax: function(a) {
                a = a || {};
                var data = {
                    url: a.url,
                    header: a.header,
                    method: a.type
                }
                var dataType = a.dataType || "json"
                if (a.type.toLowerCase() == "post") {
                    data.bodyParams = a.data;
                } else {
                    data.urlParams = a.data;
                }
                if (dataType == 'json') {
                    a._complete = function(re) {
                        var str, finalObject;
                        str = re.result
                        if (true) {
                            try {
                                finalObject = JSON.parse(str);
                            } catch (e) {
                                finalObject = str;
                            } finally {
                                re.result = finalObject;
                            }
                        }
                    }
                }
                runInvoke("sendHttp", data, a)
            },
            getClientInfo: function(a) {
                a = a || {};
                var data = {}
                if (isQTClient) {
                    if (!notMobile) {
                        if (isAndroid) {
                            data.type = 'android'
                        } else if (isIos) {
                            data.type = 'ios'
                        }
                    } else {
                        data.type = 'pc'
                    }
                } else {
                    data.type = 'not QT client'
                }
                data.version = clientVersion
                if (a.success) {
                    a.success(data)
                }
                return data
            }
        };
        if (qtjs) {
            bWindow.qt = bWindow.jQingTui = H
        }
        return H
    }
});