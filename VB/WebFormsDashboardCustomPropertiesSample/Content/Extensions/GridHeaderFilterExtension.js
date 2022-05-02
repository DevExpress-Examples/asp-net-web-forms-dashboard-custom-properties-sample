var GridHeaderFilterExtension = (function () {
    var Model = DevExpress.Dashboard.Model;
    var Designer = DevExpress.Dashboard.Designer;

    // 1. Model
    var enabledHeaderFilterProperty = {
        ownerType: Model.GridItem,
        propertyName: 'headerFilterEnabled',
        defaultValue: false,
        valueType: 'boolean'
    };

    Model.registerCustomProperty(enabledHeaderFilterProperty);

    // 2. Viewer
    function onItemWidgetOptionsPrepared(args) {
        if (args.dashboardItem instanceof Model.GridItem) {
            args.options['headerFilter'] = {
                visible: args.dashboardItem.customProperties.getValue(enabledHeaderFilterProperty.propertyName)
            };
        }
    };

    // 3. Designer
    function onCustomizeSections(args) {
        args.addSection({
            title: 'Header Filter (Custom)',
            items: [{
                dataField: enabledHeaderFilterProperty.propertyName,
                label: {
                    text: 'Header Filter'
                },
                template: Designer.FormItemTemplates.buttonGroup,
                editorOptions: {
                    keyExpr: 'value',
                    items: [{
                        value: true,
                        text: 'On'
                    }, {
                        value: false,
                        text: 'Off'
                    }]
                }
            }]
        });
    };

    // 4. Event Subscription
    function GridHeaderFilterExtension(dashboardControl) {
        this.name = "GridHeaderFilter",
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
    return GridHeaderFilterExtension;
}());