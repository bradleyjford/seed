(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.service('SecurityPrincipal', ['$rootScope', function ($rootScope) {
        this.isAuthenticated = false;

        this.identity = {
            username: '',
            displayName: ''
        };

        this.roles = [];

        this.set = function (username, displayName, roles) {
            this.isAuthenticated = true;

            this.identity.username = username;
            this.identity.displayName = displayName;

            this.roles.length = 0;
            this.roles.push.apply(this.roles, roles);

            $rootScope.$broadcast('securityPrincipalSet', this.roles);
        };

        this.clear = function () {
            this.isAuthenticated = false;

            this.identity.username = '';
            this.identity.displayName = '';

            this.roles.length = 0;

            $rootScope.$broadcast('securityPrincipalCleared');
        };

        this.isInRole = function (role) {
            return this.roles.indexOf(role) !== -1;
        };
    }]);
})(angular);
