var seedApp = (function (angular) {
    'use strict';

    var app = angular.module('seedApp', [
        'ui.router',
        'ngAnimate',
        'ngResource',

        'LocalStorageModule',

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

            $urlRouterProvider.when('', '/');
            $urlRouterProvider.otherwise('/not-found');

            $stateProvider
                .state('app', {
                    abstract: true,
                    template: '<ui-view></ui-view>',
                    resolve: {
                        user: ['$state', 'SecurityPrincipal', function ($state, SecurityPrincipal) {
                            return SecurityPrincipal.getCurrent()
                                .error(function (data, status) {
                                    if (status === 404) {
                                        $state.go('sign-in');
                                    }
                                }).$promise;
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

            $provide.factory('AuthorizationHttpInterceptor', ['$q', '$injector',
                function ($q, $injector) {
                    function responseError (rejection) {
                        var $state = $injector.get('$state');

                        if (rejection.status === 401 && $state.current.name !== 'sign-in') {
                            $state.go('sign-in');
                        }
                        else if (rejection.status === 403) {
                            $state.go('403');
                        }

                        return $q.reject(rejection);
                    }

                    return {
                        responseError: responseError
                    };
                }
            ]);

            $provide.factory('HttpEventInterceptor', ['$q', '$rootScope',
                function ($q, $rootScope) {
                    function requestHandler(config) {
                        $rootScope.$broadcast('httpRequest', config);

                        return config || $q.when(config);
                    }

                    function requestErrorHandler(rejection) {
                        $rootScope.$broadcast('httpRequestError', rejection);

                        return $q.reject(rejection);
                    }

                    function responseHandler(response) {
                        $rootScope.$broadcast('httpResponse', response);

                        return response || $q.when(response);
                    }

                    function responseErrorHandler(rejection) {
                        $rootScope.$broadcast('httpResponseError', rejection);

                        return $q.reject(rejection);
                    }

                    return {
                        'request': requestHandler,
                        'requestError': requestErrorHandler,
                        'response': responseHandler,
                        'responseError': responseErrorHandler
                    };
                }
            ]);

            $httpProvider.interceptors.push('HttpEventInterceptor');
            $httpProvider.interceptors.push('AuthorizationHttpInterceptor');
        }]);

    app.run(['$rootScope', 'RouteMediator', 'SecurityPrincipal',
        function ($rootScope, RouteMediator, SecurityPrincipal) {
            $rootScope.user = SecurityPrincipal;

            RouteMediator.setRoutingHandlers();
        }]);

    angular.module('seedApp.templates', []);

    return app;
})(angular);