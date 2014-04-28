var seedApp = (function (angular, toastr) {
    'use strict';

    var app = angular.module('seedApp', [
        'ui.router',
        'ngAnimate',
        'ngResource',

        'seedApp.templates',

        'seedApp.home',
        'seedApp.admin',

        'ui.bootstrap.tpls',
        'ui.bootstrap'
    ]);

    app.config([
        '$stateProvider', '$urlRouterProvider', '$httpProvider', '$locationProvider', '$provide',
        function ($stateProvider, $urlRouterProvider, $httpProvider, $locationProvider, $provide) {
            $locationProvider.html5Mode(false);
            $locationProvider.hashPrefix('!');

            $urlRouterProvider.otherwise('/not-found');

            $stateProvider
                .state('404', {
                    url: '/not-found',
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
                });

            $provide.factory('AuthorizationHttpInterceptor', [
                '$q', '$location', '$injector',

                function ($q, $location, $injector) {
                    var responseError = function (rejection) {
                        var $state = $injector.get('$state');

                        if (rejection.status === 401) {
                            $state.go('sign-in', { returnUrl: $location.url() });
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

            $httpProvider.interceptors.push('AuthorizationHttpInterceptor');
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