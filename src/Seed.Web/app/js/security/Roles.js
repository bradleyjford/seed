seedApp.security = seedApp.security || { };

(function (ns) {
    'use strict';

    var Roles = function () {
    };

    Roles.prototype.admin = 'admin';

    ns.Roles = Roles;
})(seedApp.security);