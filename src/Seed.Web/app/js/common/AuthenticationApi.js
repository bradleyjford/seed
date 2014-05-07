(function (angular, jquery) {
    'use strict';

    var module = angular.module('seedApp');

    module.factory('AuthenticationApi', ['$http', 'localStorageService', function($http, localStorageService) {

        function signIn(username, password) {
            return $http.post('/api/token', jquery.param({
                    grant_type: 'password',
                    username: username,
                    password: password
                }),
                {
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    }
                })
                .success(function (data) {
                    $http.defaults.headers.common.Authorization = 'Bearer ' + data.access_token;
                });
        }

        function signOut() {
            localStorageService.remove('access_token');

            $http.defaults.headers.common.Authorization = '';
        }

        function getIdentity() {
            return $http.get('/api/identity');
        }

        return {
            signIn: signIn,
            signOut: signOut,
            getIdentity: getIdentity
        };
    }]);
})(angular, $);