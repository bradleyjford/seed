(function (angular) {
    'use strict';

    var seedApp = angular.module('seedApp', [
        'ngRoute',
        'seedApp.templates',
        'seedApp.security',
        'seedApp.navigation',
        'ui-bootstrap.templates',
        'ui.bootstrap'
    ]);

    seedApp.config([
        '$routeProvider', '$httpProvider', '$locationProvider', '$provide',
        function ($routeProvider, $httpProvider, $locationProvider, $provide) {
            $locationProvider.html5Mode(false);
            $locationProvider.hashPrefix('!');

            $routeProvider.when('/', {
                templateUrl: 'security/SignIn.html',
                controller: 'SignInCtrl'
            });

            $routeProvider.when('/signout', {
                templateUrl: 'security/SignOut.html',
                controller: 'SignOutCtrl'
            });

            $routeProvider.when('/unauthorized', { templateUrl: 'security/Unauthorized.html' });

            $routeProvider.when('/test', {
                templateUrl: 'security/SignOut.html',
                controller: 'TestCtrl'
            });

            $routeProvider.otherwise({ templateUrl: 'common/404.html' });

            $provide.factory('AuthorizationHttpInterceptor', [
                '$q', '$location',
                function ($q, $location) {
                    return {
                        responseError: function (rejection) {
                            if (rejection.status === 401) {
                                $location.path('/');
                            }
                            else if (rejection.status === 403) {
                                $location.path('/unauthorized');
                            }

                            return $q.reject(rejection);
                        }
                    };
                }]);

            $httpProvider.interceptors.push('AuthorizationHttpInterceptor');
        }]);

    angular.module('seedApp.templates', []);
})(angular);