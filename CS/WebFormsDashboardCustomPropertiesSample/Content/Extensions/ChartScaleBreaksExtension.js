var ChartScaleBreaksExtension = (function() {
    var Model = DevExpress.Dashboard.Model;

    // 1. Model
    var autoScaleBreaksProperty = {
        ownerType: Model.ChartItem,
        propertyName: 'ScaleBreaks',
        defauleValue: false,
        valueType: 'boolean'
    };

    Model.registerCustomProperty(autoScaleBreaksProperty);

    // 2. Viewer
    function onItemWidgetOptionsPrepared(args) {
        var scaleBreaks = args.dashboardItem.customProperties.getValue(autoScaleBreaksProperty.propertyName);
        if (scaleBreaks) {
            var valueAxisOptions = args.options["valueAxis"];
            if (valueAxisOptions && valueAxisOptions[0]) {
                valueAxisOptions[0].autoBreaksEnabled = true;
            }
        }
    };

    // 3. Designer
    function onCustomizeSections(args) {
        args.addSection({
            title: "Scale breaks (Custom)",
            items: [
                {
                    dataField: autoScaleBreaksProperty.propertyName,
                    editorType: "dxCheckBox",
                    label: {
                        visible: false
                    },
                    editorOptions: {
                        text: "Enable Auto Scale breaks"
                    }
                }
            ]
        });
    };
    // 4. Event Subscription
    function ChartScaleBreaksExtension(dashboardControl) {
        this.name = "ScaleBreaks",
        this.start = function () {
            var viewerApiExtension = dashboardControl.findExtension('viewer-api');
            if (viewerApiExtension) {
                viewerApiExtension.on('itemWidgetOptionsPrepared', onItemWidgetOptionsPrepared);
            }
            var optionsPanelExtension = dashboardControl.findExtension("item-options-panel");
            if (optionsPanelExtension) {
                optionsPanelExtension.on('customizeSections', onCustomizeSections);
            }
        },
        this.stop = function () {
            var viewerApiExtension = dashboardControl.findExtension('viewer-api');
            if (viewerApiExtension) {
                viewerApiExtension.off('itemWidgetOptionsPrepared', onItemWidgetOptionsPrepared);
            }
            var optionsPanelExtension = dashboardControl.findExtension("item-options-panel");
            if (optionsPanelExtension) {
                optionsPanelExtension.off('customizeSections', onCustomizeSections);
            }
        }
    }
    return ChartScaleBreaksExtension;
}());