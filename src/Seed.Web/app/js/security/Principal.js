seedApp.security = seedApp.security || { };

(function (ns) {
    'use strict';

    var Principal = function () {
        this.isAuthenticated = false;
        this.name = '';
        this.roles = [];
    };

    Principal.prototype.signIn = function (name, roles) {
        this.isAuthenticated = true;
        this.name = name;

        this.roles.push.apply(this.roles, roles);
    };

    Principal.prototype.signOut = function () {
        this.isAuthenticated = false;
        this.name = '';
        this.roles.length = 0;
    };

    Principal.prototype.isInRole = function (role) {
        return this.roles.indexOf(role) !== -1;
    };

    ns.Principal = Principal;
})(seedApp.security);
