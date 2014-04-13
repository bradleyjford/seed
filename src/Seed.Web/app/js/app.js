var seedApp = (function (angular) {
    'use strict';

    var seedApp = angular.module('seedApp', [
        'ngRoute',
        'seedApp.templates',
        'seedApp.security',
        'seedApp.navigation',
        'seedApp.dashboard',
        'ui-bootstrap.templates',
        'ui.bootstrap'
    ]);

    seedApp.config([
        '$routeProvider', '$httpProvider', '$locationProvider', '$provide',
        function ($routeProvider, $httpProvider, $locationProvider, $provide) {
            $locationProvider.html5Mode(false);
            $locationProvider.hashPrefix('!');

            $routeProvider.when('/signin', {
                templateUrl: 'security/SignIn.html',
                controller: 'SignInCtrl',
                title: 'Sign in'
            });

            $routeProvider.when('/signout', {
                templateUrl: 'security/SignOut.html',
                controller: 'SignOutCtrl',
                title: 'Sign out'
            });

            $routeProvider.when('/', {
                templateUrl: 'dashboard/Dashboard.html',
                controller: 'DashboardCtrl',
                requireRole: 'admin',
                title: 'Dashboard'
            });

            $routeProvider.when('/admin', {
                templateUrl: 'security/SignOut.html',
                controller: 'TestCtrl',
                requireRole: 'admin',
                title: 'Admin'
            });

            $routeProvider.when('/unauthorized', {
                templateUrl: 'security/Unauthorized.html',
                title: 'Access denied'
            });

            $routeProvider.otherwise({
                templateUrl: 'common/404.html',
                title: 'Not found'
            });

            $provide.factory('AuthorizationHttpInterceptor', [
                '$q', '$location',

                function ($q, $location) {
                    var responseError = function (rejection) {
                        if (rejection.status === 401) {
                            $location.path('/signin');
                        }
                        else if (rejection.status === 403) {
                            $location.path('/unauthorized');
                        }

                        return $q.reject(rejection);
                    };

                    return {
                        responseError: responseError
                    };
                }
            ]);

            $httpProvider.interceptors.push('AuthorizationHttpInterceptor');
        }]);

    angular.module('seedApp.templates', []);

    return seedApp;
})(angular);