var seedApp = (function (angular, toastr) {
    'use strict';

    var app = angular.module('seedApp', [
        'ui.router',
        'ngAnimate',
        'seedApp.templates',
        'seedApp.security',
        'seedApp.dashboard',
        'ui.bootstrap.tpls',
        'ui.bootstrap'
    ]);

    app.config([
        '$stateProvider', '$urlRouterProvider', '$httpProvider', '$locationProvider', '$provide',
        function ($stateProvider, $urlRouterProvider ,$httpProvider, $locationProvider, $provide) {
            $locationProvider.html5Mode(false);
            $locationProvider.hashPrefix('!');

            $urlRouterProvider.otherwise('/not-found');

            $stateProvider
                .state('404', {
                    url: 'not-found',
                    templateUrl: 'common/404.html',
                    data: {
                        title: 'Not found'
                    }
                })

                .state('403', {
                    url: '/unauthorized',
                    templateUrl: 'common/403.html',
                    data: {
                        title: 'Unauthorized'
                    }
                })

                .state('sign-in', {
                    url: '/sign-in',
                    templateUrl: 'security/SignIn.html',
                    controller: 'SignInCtrl',
                    data: {
                        title: 'Sign in'
                    }
                })

                .state('sign-out', {
                    url: '/sign-out',
                    templateUrl: 'security/SignOut.html',
                    controller: 'SignOutCtrl',
                    data: {
                        title: 'Sign out'
                    }
                })

                .state('home', {
                    url: '/',
                    templateUrl: 'dashboard/Dashboard.html',
                    controller: 'DashboardCtrl',
                    data: {
                        title: 'Dashboard'
                    }
                });

            $provide.factory('AuthorizationHttpInterceptor', [
                '$q', '$injector',

                function ($q, $injector) {
                    var $state = $injector.get('$state');

                    var responseError = function (rejection) {
                        if (rejection.status === 401) {
                            $state.go('sign-in');
                        }
                        else if (rejection.status === 403) {
                            $state.go('403');
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

            //$httpProvider.interceptors.push('AuthorizationHttpInterceptor');
            $httpProvider.interceptors.push('HttpEventInterceptor');
        }]);

    angular.module('seedApp.templates', []);

    toastr.options = {


        iconClasses: {
            error: 'alert-error',
            info: 'alert-info',
            success: 'alert-success',
            warning: 'alert-warning'
        },

        "closeButton": true,
        "debug": false,
        "positionClass": "toast-bottom-right",
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

    return app;
})(angular, toastr);