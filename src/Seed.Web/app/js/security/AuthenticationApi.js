(function (angular) {
    'use strict';

    var AuthenticationApi = function ($http) {
        this.$http = $http;
    };

    AuthenticationApi.prototype.signIn = function (userName, password) {
        var data = {
            userName: userName,
            password: password
        };

        return this.$http.post('/api/authentication/signin', data);
    };

    AuthenticationApi.prototype.signOut = function () {
        return this.$http.post('/api/authentication/signout');
    };

    AuthenticationApi.prototype.get = function () {
        return this.$http.post('/api/authentication/principal');
    };

    AuthenticationApi.prototype.test = function () {
        return this.$http.get('/api/authentication/test');
    };

    angular.module('seedApp.security')
        .factory('AuthenticationApi', ['$http', function ($http) {
            return new AuthenticationApi($http);
        }]);
})(angular);