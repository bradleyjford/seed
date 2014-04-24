module.exports = function (grunt) {
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-less');
    grunt.loadNpmTasks('grunt-env');
    grunt.loadNpmTasks('grunt-preprocess');
    grunt.loadNpmTasks('grunt-recess');
    grunt.loadNpmTasks('grunt-karma');
    grunt.loadNpmTasks('grunt-html2js');
    grunt.loadNpmTasks('grunt-concurrent');

    grunt.registerTask('default', ['env:dev', 'timestamp', 'clean', 'html2js', 'jshint', 'preprocess', 'uglify', 'copy', 'less']);
    grunt.registerTask('release', ['env:release', 'timestamp', 'clean', 'html2js', 'jshint', 'preprocess', 'uglify', 'copy', 'less']);

    grunt.registerTask('timestamp', function () {
        grunt.log.subhead(Date());
    });

    var karmaConfig = function (configFile, customOptions) {
        var options = { configFile: configFile, keepalive: true };
        var travisOptions = process.env.TRAVIS && { browsers: ['Firefox'], reporters: 'dots' };
        return grunt.util._.extend(options, customOptions, travisOptions);
    };

    grunt.initConfig({
        distDir: '../../dist',
        libDir: '../../lib',
        env: {
            options: {
            },
            dev: {
                NODE_ENV: 'dev'
            },
            release: {
                NODE_ENV: 'release'
            }
        },
        pkg: grunt.file.readJSON('package.json'),
        src: {
            app: ['app/js/app.js'],
            js: ['app/js/**/*.js', '!(app/js/app.js)', '!(app/js/*Module.js)'],
            modules: ['app/js/**/*Module.js'],
            assets: [],
            tpl: ['app/js/**/*.html'],
            appJs: ['<%= src.app %>', '<%= src.modules %>', '<%= src.js %>'],
            specs: ['test/**/*.spec.js'],
            scenarios: ['test/**/*.scenario.js']
        },
        clean: {
            options: {
                force: true
            },
            files: ['<%= distDir %>/*']
        },
        copy: {
            assets: {
                files: [
                    { dest: '<%= distDir %>', src: ['**', '!(**/*.less)'], expand: true, cwd: 'app/assets' }
                ]
            },
            spaHost: {
                files: [
                    { dest: '<%= distDir %>', src: '**', expand: true, cwd: '../../lib/Seed.Web.SpaHost' }
                ]
            },
            lib: {
                files: [
                    { dest: '<%= distDir %>/js', src: ['*.js', '*.map'], expand: true, cwd: 'bower_components/angular' },
                    { dest: '<%= distDir %>/css', src: ['*.css'], expand: true, cwd: 'bower_components/angular' },
                    { dest: '<%= distDir %>/js', src: ['*.js', '*.map'], expand: true, cwd: 'bower_components/angular-animate' },
                    { dest: '<%= distDir %>/js', src: ['*.js', '*.map'], expand: true, cwd: 'bower_components/angular-resource' },
                    { dest: '<%= distDir %>/js', src: ['*.js', '*.map'], expand: true, cwd: 'bower_components/angular-bootstrap' },
                    { dest: '<%= distDir %>/js', src: ['*.js', '*.map'], expand: true, cwd: 'bower_components/angular-ui-router/release' },
                    { dest: '<%= distDir %>/js', src: ['*.js', '*.map'], expand: true, cwd: 'bower_components/toastr' },
                    { dest: '<%= distDir %>/css', src: ['*.css'], expand: true, cwd: 'bower_components/toastr' },
                    { dest: '<%= distDir %>/css', src: ['*.css'], expand: true, cwd: 'bower_components/animate.css' },
                    { dest: '<%= distDir %>', src: ['**/*'], expand: true, cwd: 'bower_components/bootstrap/dist' },
                    { dest: '<%= distDir %>/js', src: ['*'], expand: true, cwd: 'bower_components/jquery/dist' },
                    { dest: '<%= distDir %>/fonts', src: ['*'], expand: true, cwd: 'lib/font-awesome-4.0.3/fonts' },
                    { dest: '<%= distDir %>/css', src: ['*'], expand: true, cwd: 'lib/font-awesome-4.0.3/css' }
                ]
            }
        },
        karma: {
            unit: { options: karmaConfig('config/karma.conf.js') },
            e2e: { options: karmaConfig('config/karma-e2e.conf.js') },
            watch: { options: karmaConfig('config/karma.conf.js', { singleRun: false, autoWatch: true }) }
        },
        jshint: {
            files: ['Gruntfile.js', '<%= src.appJs %>'],
            options: {
                curly: true,
                eqeqeq: true,
                immed: true,
                latedef: true,
                newcap: true,
                noarg: true,
                sub: true,
                boss: true,
                eqnull: true,
                globals: {}
            }
        },
        html2js: {
            app: {
                options: {
                    base: 'app/js'
                },
                src: ['<%= src.tpl %>'],
                dest: '<%= distDir %>/js/SeedApp.html.js',
                module: 'seedApp.templates'
            }
        },
        uglify: {
            options: {
                sourceMap: true
            },
            app: {
                options: {
                    sourceMapIncludeSources: true
                },
                files: {

                    '../../dist/js/SeedApp.min.js': ['<%= src.appJs %>']

                }
            }
        },
        less: {
            app: {
                files: {
                    '<%= distDir %>/css/seed.css': 'app/assets/less/app.less'
                }
            }
        },
        preprocess: {
            options: {

            },
            html: {
                src: 'app/index.html',
                dest: '<%= distDir %>/Views/Home/Index.cshtml'
            }
        },
        watch: {
            appLess: {
                files: ['app/assets/less/**.less', '!(app/assets/less/bootstrap/*.less)'],
                tasks: ['less:app']
            },
            appJs: {
                files: ['app/js/**/*.js'],
                tasks: ['jshint', 'uglify:app']
            },
            index: {
                files: ['app/index.html'],
                tasks: ['preprocess:html']
            },
            templates: {
                files: ['app/js/**/*.html'],
                tasks: ['html2js:app']
            }
        },
        concurrent: {
            target: {
                tasks: ['rebuild', 'watch'],
                options: {
                  logConcurrentOutput: true,
                  NODE_ENV: 'dev'
                }
            }
        }
    });
};