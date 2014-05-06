(function (angular, jquery) {
    'use strict';

    var AuthenticationApi = function AuthenticationApi ($http) {
        this.$http = $http;
    };

    AuthenticationApi.prototype.signIn = function (username, password) {
        return this.$http.post('/api/token', jquery.param({
                grant_type: 'password',
                username: username,
                password: password
            }),
            {
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            }
        );
    };

    AuthenticationApi.prototype.signOut = function () {
        return this.$http.post('/api/authentication/signout');
    };

    AuthenticationApi.prototype.getSecurityPrincipal = function () {
        return this.$http.post('/api/authentication/identity');
    };

    AuthenticationApi.prototype.test = function () {
        return this.$http.get('/api/authentication/test');
    };

    AuthenticationApi.$inject = ['$http'];

    angular.module('seedApp')
        .service('AuthenticationApi', AuthenticationApi);
})(angular, $);