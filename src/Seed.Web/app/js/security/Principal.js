(function (angular) {
    'use strict';

    var Principal = function ($rootScope) {
        this.$rootScope = $rootScope;

        this.isAuthenticated = false;
        this.name = '';
        this.fullName = '';
        this.roles = [];
    };

    Principal.prototype.signIn = function (username, fullName, roles) {
        this.isAuthenticated = true;
        this.name = username;
        this.fullName = fullName;

        this.roles.length = 0;
        this.roles.push.apply(this.roles, roles);

        this.$rootScope.$broadcast('seedApp.signIn', this.roles);
    };

    Principal.prototype.signOut = function () {
        this.isAuthenticated = false;
        this.name = '';
        this.fullName = '';
        this.roles.length = 0;

        this.$rootScope.$broadcast('seedApp.signOut');
    };

    Principal.prototype.isInRole = function (role) {
        return this.roles.indexOf(role) !== -1;
    };

    var module = angular.module('seedApp');

    module.service('Principal', ['$rootScope', Principal]);
})(angular);
