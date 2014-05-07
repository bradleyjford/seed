(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.factory('SecurityPrincipal', ['AuthenticationApi', '$rootScope', function (AuthenticationApi, $rootScope) {
        var _isAuthenticated = false;

        var _identity = {
            username: '',
            displayName: ''
        };

        var _roles = [];

        function signIn(username, password) {
            clear();

            var result = AuthenticationApi.signIn(username, password)
                .success(function (data) {
                    set(data.username, data.fullName, data.roles);

                    $rootScope.$broadcast('securityPrincipalSignedIn');
                });

            return result;
        }

        function signOut() {
            var result = AuthenticationApi.signOut()
                .success(function () {
                    clear();

                    $rootScope.$broadcast('securityPrincipalSignedOut');
                });

            return result;
        }

        function getCurrent() {
            var result = AuthenticationApi.getIdentity()
                .success(function (data) {
                    set(data.username, data.fullName, data.roles);

                    $rootScope.$broadcast('securityPrincipalSignedIn', data.roles);
                });

            return result;
        }

        function isInRole(role) {
            return _roles.indexOf(role) !== -1;
        }

        function set(username, displayName, roles) {
            _isAuthenticated = true;

            _identity.username = username;
            _identity.displayName = displayName;

            _roles.length = 0;
            _roles.push.apply(_roles, roles);
        }

       function clear() {
            _isAuthenticated = false;

            _identity.username = '';
            _identity.displayName = '';

            _roles.length = 0;
        }

        var service = {
            isAuthenticated: _isAuthenticated,
            identity: _identity,
            roles: _roles,

            signIn: signIn,
            signOut: signOut,
            getCurrent: getCurrent,
            isInRole: isInRole
        };

        return service;
    }]);
})(angular);
