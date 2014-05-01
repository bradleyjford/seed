(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.service('SecurityPrincipal', ['AuthenticationApi', '$rootScope', function (AuthenticationApi, $rootScope) {
        this.isAuthenticated = false;

        this.identity = {
            username: '',
            displayName: ''
        };

        this.roles = [];

        var self = this;

        this.signIn = function (username, password) {
            clear();

            var result = AuthenticationApi.signIn(username, password)
                .success(function (data) {
                    set(data.username, data.fullName, data.roles);

                    $rootScope.$broadcast('securityPrincipalSignedIn');
                });

            return result;
        };

        this.signOut = function () {
            var result = AuthenticationApi.signOut()
                .success(function () {
                    clear();

                    $rootScope.$broadcast('securityPrincipalSignedOut');
                });

            return result;
        };

        this.getCurrent = function () {
            var result = AuthenticationApi.getSecurityPrincipal()
                .success(function (data) {
                    set(data.username, data.fullName, data.roles);

                    $rootScope.$broadcast('securityPrincipalSignedIn', data.roles);
                });

            return result;
        };

        var set = function (username, displayName, roles) {
            self.isAuthenticated = true;

            self.identity.username = username;
            self.identity.displayName = displayName;

            self.roles.length = 0;
            self.roles.push.apply(self.roles, roles);
        };

        var clear = function () {
            self.isAuthenticated = false;

            self.identity.username = '';
            self.identity.displayName = '';

            self.roles.length = 0;
        };

        this.isInRole = function (role) {
            return this.roles.indexOf(role) !== -1;
        };
    }]);
})(angular);
