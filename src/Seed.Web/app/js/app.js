var seedApp = (function (angular) {
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
                .state('app', {
                    abstract: true,
                    controller: 'AppController',
                    template: '<ui-view></ui-view>',
                    resolve: {
                        user: ['SecurityPrincipal', function (SecurityPrincipal) {
                            return SecurityPrincipal.getCurrent().$promise;
                        }]
                    }
                })

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

                        if (rejection.status === 401 && $state.current.name !== 'sign-in') {
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

            $httpProvider.interceptors.push('AuthorizationHttpInterceptor');
        }]);

    angular.module('seedApp.templates', []);

    return app;
})(angular);