(function (ns) {
    'use strict';

    var Alert = function (type, message) {
        this.type = type;
        this.message = message;
    };

    Alert.prototype.success = 'success';
    Alert.prototype.info = 'info';
    Alert.prototype.warning = 'warning';
    Alert.prototype.danger = 'danger';

    ns.Alert = Alert;
})(seedApp);