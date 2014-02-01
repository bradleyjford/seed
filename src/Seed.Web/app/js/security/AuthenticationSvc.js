(function (angular) {
    'use strict';

    angular.module('seedApp.security')
        .service('AuthenticationSvc', ['$http', function ($http) {
            var self = this;
            self.isAuthenticated = false;

            var signIn = function (userName, password) {
                var data = {
                    userName: userName,
                    password: password
                };

                return $http.post('/api/authentication/signin', data)
                    .success(function () {
                        self.isAuthenticated = true;
                    });
            };

            var signOut = function () {
                return $http.post('/api/authentication/signout')
                    .success(function () {
                        self.isAuthenticated = false;
                    });
            };

            var isAuthenticated = function () {
                return self.isAuthenticated;
            };

            var test = function () {
                return $http.get('/api/authentication/test');
            };

            return {
                signIn: signIn,
                signOut: signOut,
                isAuthenticated: isAuthenticated,
                test: test
            };
        }]);
})(angular);