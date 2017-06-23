var Core = (function () {
    function Core() { }

    function rebindEvent(event, selector, callback) {
        $(document).off(event, selector);
        $(document).on(event, selector, callback);
    }

    function getUrl(actionName, controllerName) {
        return "/MyGuide/" + controllerName + "/" + actionName;
    }

    function loadResources() {
        $.ajax({
            success: function (data) {
                core.Resources = data;
            },
            url: getUrl("GetResources", "Home")
        });
    }

    function displayPopUp(data) {
        var popup = document.getElementById("info-popup");
        $(popup).find(".popup-content").html(data);
        popup.classList.toggle("show");
    };

    function manageSearchButton() {
        var mapButton = $(".bottom-button-container");
        if ($(".chosenItems-content").length) {
            if (mapButton.hasClass("hidden")) {
                mapButton.removeClass("hidden");
            }
        } else {
            if (!mapButton.hasClass("hidden")) {
                mapButton.addClass("hidden");
            }
        }
    }

    function rebindEvents() {
        rebindEvent("click", "button.add", function () {
            var itemId = $(this).closest(".dest-actions").find("input[name*='itemId']").val();

            $.ajax({
                success: function (data) {
                    $("#chosenItems").html(data);
                    manageSearchButton();
                },
                url: "/MyGuide/Plan/AddItem",
                data: {
                    itemId: itemId
                }
            });
        });

        rebindEvent("click", "button.remove", function () {
            var itemId = $(this).closest(".dest-actions").find("input[name*='itemId']").val();

            $.ajax({
                success: function (data) {
                    $("#chosenItems").html(data);
                    manageSearchButton();
                },
                url: "/MyGuide/Plan/RemoveItem",
                error: function () {
                    $.ajax({
                        success: function (data) {
                            displayPopUp(data);
                        },
                        url: "/MyGuide/Routing/RemoveItem",
                        data: {
                            itemId: itemId
                        }
                    });
                },
                data: {
                    itemId: itemId
                }
            });
        });

        rebindEvent("click", "button.info", function () {
            var itemId = $(this).closest(".dest-actions").find("input[name*='itemId']").val();

            $.ajax({
                success: function (data) {
                    displayPopUp(data);
                },
                error: function () {
                    $.ajax({
                        success: function (data) {
                            displayPopUp(data);
                        },
                        url: "/MyGuide/Routing/GetDestinationInfo",
                        data: {
                            itemId: itemId
                        }
                    });
                },
                url: "/MyGuide/Plan/GetDestinationInfo",
                data: {
                    itemId: itemId
                }
            });
        });
        
        rebindEvent("click", "#go-to-map-js", function () {
            var homePlaceSelect = $(this).closest(".bottom-button-container").find("select[name='homeTown']");
            var homePlaceId = homePlaceSelect.val();

            if (!homePlaceId) {
                alert(core.Resources.MustChooseStartPoint);
                return;
            }

            window.location = "/MyGuide/Routing/Index?townId=" + homePlaceId;
        });
        
        rebindEvent("click", "#save-route-js", function () {
            window.location = "/MyGuide/Routing/Save";
        });

        rebindEvent("click", "#info-popup .close-button", function () {
            var popup = $("#info-popup");
            if (popup.hasClass("show")) {
                popup.removeClass("show");
            }
        });
    }

    Core.prototype.Resources = {}

    Core.prototype.Init = function () {
        loadResources();

        rebindEvents()
    }

    Core.prototype.RebindEvent = rebindEvent;

    Core.prototype.DisplayPopUp = displayPopUp;

    return Core;
}());

core = new Core();