(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin');

    module.controller('UserEditController',
        ['$scope', '$state', '$modal', 'UsersApi', 'model',
        function ($scope, $state, $modal, UsersApi, model) {
        $scope.model = model;

        $scope.save = function () {
            if ($scope.editUserForm.$valid)
            {
                $scope.model.$save({ userId: $scope.model.id }, function () {
                    $state.go('^', null, { reload: true });
                });
            }
        };

        $scope.enable = function () {
            var modalInstance = $modal.open({
                templateUrl: 'enableUser.tpl',
                controller: ['$scope', '$modalInstance', 'userName', IsActiveUserController],
                resolve: {
                    userName: function () {
                        return 'Brad';
                    }
                }
            });

            modalInstance.result.then(function () {
                UsersApi.activate({ userId: $scope.model.id }, null, function () {
                    $scope.model.isActive = true;
                });
            });
        };

        $scope.disable = function () {
            var modalInstance = $modal.open({
                templateUrl: 'disableUser.tpl',
                controller: ['$scope', '$modalInstance', 'userName', IsActiveUserController],
                resolve: {
                    userName: function () {
                        return 'Brad';
                    }
                }
            });

            modalInstance.result.then(function () {
                UsersApi.deactivate({ userId: $scope.model.id }, null, function () {
                    $scope.model.isActive = false;
                });
            });
        };

        $scope.cancel = function() {
            $state.go('^');
        };
    }]);

    var IsActiveUserController = function ($scope, $modalInstance, userName) {
        $scope.userName = userName;

        $scope.yes = function () {
            $modalInstance.close(true);
        };

        $scope.no = function () {
            $modalInstance.dismiss('cancel');
        };
    };

})(angular);