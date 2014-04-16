var seedApp = (function (angular, toastr) {
    'use strict';

    var seedApp = angular.module('seedApp', [
        'ngRoute',
        'ngAnimate',
        'seedApp.templates',
        'seedApp.security',
        'seedApp.navigation',
        'seedApp.dashboard',
        'ui.bootstrap.tpls',
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

            $routeProvider.when('/unauthorized', {
                templateUrl: 'security/Unauthorized.html',
                title: 'Access denied'
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

            $provide.factory('HttpEventInterceptor', [
                '$q', '$rootScope',

                function ($q, $rootScope) {
                    var request = function (config) {
                        $rootScope.$broadcast('httpRequest', config);

                        return config || $q.when(config);
                    };

                    var requestError = function (rejection) {
                        $rootScope.$broadcast('httpRequestError', rejection);

                        return $q.reject(rejection);
                    };

                    var response = function (response) {
                        $rootScope.$broadcast('httpResponse', response);

                        return response || $q.when(response);
                    };

                    var responseError = function (rejection) {
                        $rootScope.$broadcast('httpResponseError', rejection);

                        return $q.reject(rejection);
                    };

                    return {
                        'request': request,
                        'requestError': requestError,
                        'response': response,
                        'responseError': responseError
                    };
                }
            ]);

            $httpProvider.interceptors.push('AuthorizationHttpInterceptor');
            $httpProvider.interceptors.push('HttpEventInterceptor');
        }]);

    angular.module('seedApp.templates', []);

    toastr.options = {
/* Apply Bootstrap styling to toastr
        toastClass: 'alert',
        iconClasses: {
            error: 'alert-error',
            info: 'alert-info',
            success: 'alert-success',
            warning: 'alert-warning'
        },
*/
        "closeButton": true,
        "debug": false,
        "positionClass": "toast-top-full-width",
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    return seedApp;
})(angular, toastr);