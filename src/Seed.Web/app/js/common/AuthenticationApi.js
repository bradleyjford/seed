(function (angular, jquery) {
    'use strict';

    var module = angular.module('seedApp');

    module.factory('AuthenticationApi', ['$http', '$q', 'localStorageService',
        function($http, $q, localStorageService) {

        function applyAccessToken(accessToken) {
            localStorageService.add('access_token', accessToken);
            $http.defaults.headers.common.Authorization = 'Bearer ' + localStorageService.get('access_token');
        }

        function clearAccessToken() {
            $http.defaults.headers.common.Authorization = undefined;
            localStorageService.remove('access_token');
        }

        function ensureAuthorizationHeader() {
            if (!$http.defaults.headers.common.Authorization) {
                var accessToken = localStorageService.get('access_token');

                if (accessToken) {
                    $http.defaults.headers.common.Authorization = 'Bearer ' + localStorageService.get('access_token');
                }
            }
        }

        function signIn(userName, password) {
            return $http.post('/api/token', jquery.param({
                    grant_type: 'password',
                    username: userName,
                    password: password
                }),
                {
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    }
                })
                .success(function (data) {
                    applyAccessToken(data.access_token);
                });
        }

        function signOut() {
            return $q.when(clearAccessToken());
        }

        function getIdentity() {
            ensureAuthorizationHeader();

            return $http.get('/api/identity');
        }

        return {
            signIn: signIn,
            signOut: signOut,
            getIdentity: getIdentity
        };
    }]);
})(angular, $);