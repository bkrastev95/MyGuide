var Map = (function () {
    function Map() {
        initMap();
    }

    var map;

    function initMap() {
        if (!map) {
            map = new ol.Map({
                layers: [
                    new ol.layer.Tile({
                        source: new ol.source.OSM()
                    })
                ],
                target: 'map',
                controls: ol.control.defaults({
                    attributionOptions: /** @type {olx.control.AttributionOptions} */ ({
                        collapsible: false
                    })
                }).extend([
                    new ol.control.ScaleLine({
                        units: 'metric'
                    }),
                    new ol.control.FullScreen()
                ]),
                view: new ol.View({
                    center: [2794519.754761992, 5230641.257344415],
                    zoom: 7,
                })
            });
        }       

        core.RebindEvent("click", "#go-home-js", function () {
            map.getView().animate({
                center: [2794519.754761992, 5230641.257344415],
                zoom: 7
            });
        })

        drawPoints();
    }

    function drawPoints() {
        if (points && points.length) {
            var drawingLayer = new ol.layer.Vector({
                source: new ol.source.Vector(),
                style: function (feature, resolution) {
                    if (feature.getGeometry().getType() === "Point") {
                        return new ol.style.Style({
                            image: new ol.style.Circle({
                                radius: 10,
                                fill: new ol.style.Fill({ color: 'rgba(255, 0, 0, 0.3)' }),
                                stroke: new ol.style.Stroke({ color: 'red', width: 1 })
                            }),
                            text: new ol.style.Text({
                                textAlign: "center",
                                textBaseline: "middle",
                                font: "Normal 12px Arial",
                                text: feature.get("name"),
                                fill: new ol.style.Fill({ color: "#aa3300" }),
                                stroke: new ol.style.Stroke({ color: "#ffffff", width: 3 })
                            })
                        });
                    } else {
                        return [
                            new ol.style.Style({
                                stroke: new ol.style.Stroke({
                                    color: 'white',
                                    width: 5
                                })
                            }),
                            new ol.style.Style({
                                stroke: new ol.style.Stroke({
                                    color: 'red',
                                    width: 3
                                })
                            })
                        ];
                    }
                }
            })

            drawingLayer.getSource().addFeatures(getMapFeaturesFromPoints());

            map.addLayer(drawingLayer);
        }
    }

    function getMapFeaturesFromPoints() {
        var features = [];
        var lineStringCoords = [];

        points.forEach(function (p, i) {
            var coords = ol.proj.transform([p.Y, p.X], "EPSG:4326", "EPSG:3857");

            features.push(new ol.Feature({
                geometry: new ol.geom.Point(coords),
                name: p.Name.toString()
            }));

            lineStringCoords.push(coords);
        });

        features.push(new ol.Feature({
            geometry: new ol.geom.LineString(lineStringCoords)
        }));

        return features;
    }
    
    Map.prototype.Init = initMap;

    return Map;
}());

map = new Map();