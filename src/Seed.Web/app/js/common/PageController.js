(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.controller('PageController',
        ['$state', '$rootScope', '$interpolate', 'SecurityPrincipal',
        function ($state, $rootScope, $interpolate, SecurityPrincipal) {
            $rootScope.page = {
                title: ''
            };

            $rootScope.user = SecurityPrincipal;

            $rootScope.$on('$stateChangeSuccess', function (event, toState) {
                $rootScope.page.title = $interpolate(toState.data.title)($state.$current.locals.globals);
            });
        }]);
})(angular);