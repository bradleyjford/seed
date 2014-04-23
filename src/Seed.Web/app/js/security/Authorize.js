(function (angular) {
    'use strict';

    angular.module('seedApp')

        .directive('saAuthorize', [function () {
            return {
                restrict: 'A',
                scope: false,
                link: function (scope, element, attrs) {
                    var originalDisplay = element.css('display');

                    scope.$on('seedApp.signIn', function (event, roles) {
                        if (attrs.saAuthorize === '' ||
                            roles.indexOf(attrs.saAuthorize) !== -1) {

                            element.css('display', originalDisplay);
                        }
                        else {
                            element.css('display', 'none');
                        }
                    });

                    scope.$on('seedApp.signOut', function (event) {
                        element.css('display', 'none');
                    });
                }
            };
        }]);
})(angular);