(function (angular) {
    'use strict';

    var AuthenticationApi = function AuthenticationApi ($http) {
        this.$http = $http;
    };

    AuthenticationApi.prototype.signIn = function (username, password) {
        var data = {
            username: username,
            password: password
        };

        return this.$http.post('/api/authentication/signin', data);
    };

    AuthenticationApi.prototype.signOut = function () {
        return this.$http.post('/api/authentication/signout');
    };

    AuthenticationApi.prototype.getSecurityPrincipal = function () {
        return this.$http.post('/api/authentication/principal');
    };

    AuthenticationApi.prototype.test = function () {
        return this.$http.get('/api/authentication/test');
    };

    AuthenticationApi.$inject = ['$http'];

    angular.module('seedApp')
        .service('AuthenticationApi', AuthenticationApi);
})(angular);