(function (angular) {
    angular.module('seedApp.security')

        // <li s-require-role="admin"><a href="/test">Admin</a></li>
        .directive('sRequireRole', ['$rootScope', function ($rootScope) {
            return {
                restrict: 'A',
                scope: false,
                link: function (scope, element, attrs) {
                    var originalDisplay = element.css('display');

                    $rootScope.$watchCollection('user.roles', function (roles) {
                        if ($rootScope.user.isInRole(attrs.sRequireRole)) {
                            element.css('display', originalDisplay);
                        }
                        else {
                            element.css('display', 'none');
                        }
                    });
                }
            };
        }]);
})(angular);