(function (angular) {
    'use strict';

    var module = angular.module('seedApp.home');

    module.controller('SignInController', ['$scope', '$state', '$stateParams', '$location', 'AuthenticationApi', 'SecurityPrincipal',
        function ($scope, $state, $stateParams, $location, AuthenticationApi, SecurityPrincipal) {
            $scope.model = {
                username: '',
                password: '',
                error: ''
            };

            $scope.signIn = function () {
                $scope.model.error = '';

                if (!$scope.signInForm.$valid) {
                    return;
                }

                SecurityPrincipal.signIn($scope.model.username, $scope.model.password)
                    .success(function () {
                        if ($stateParams.returnUrl && $stateParams.returnUrl.length > 0) {
                            $location.url($stateParams.returnUrl);
                            return;
                        }

                        $state.go('app.home');
                    })
                    .error(function (data, status, headers, config) {
                        $scope.model.error = data.message;
                    });
            };
        }
    ]);
})(angular);