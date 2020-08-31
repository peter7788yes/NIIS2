module.exports = function(grunt) {
    // Project configuration.
    grunt.initConfig({
        // �ޥ� package.json �����Ѽ�
        pkg: grunt.file.readJSON('package.json'),
        // �]�w JavaScript ���Y task
        uglify: {
			myTaskJS: {
                options: {
                    // �]�w���Y�����Y�n���J������
                    banner: '/*! <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */\n',
                    // �ϥ� SourceMap �åB�N JS Source �P Map �ɮש�b�@�_
                    sourceMap: true,
                    sourceMapIncludeSources: true
                },
                files: [
                    {
                        expand: true, 
						cwd: 'js',						
                        // �N���O .min.js ���ɮץ����i�����Y
                        src: ['**/*.src.js','!*.min.js'],
                        dest: 'js',
						ext: ".js"
                    }
                ]
            },
			myTaskMinJS: {
                options: {
                    // �]�w���Y�����Y�n���J������
                    banner: '/*! <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */\n',
                    // �ϥ� SourceMap �åB�N JS Source �P Map �ɮש�b�@�_
                    sourceMap: true,
                    sourceMapIncludeSources: true
                },
                files: [
                    {
                        expand: true, 
						cwd: 'js',						
                        // �N���O .min.js ���ɮץ����i�����Y
                        src: ['**/*.src.js','!*.min.js'],
                        dest: 'js',
						ext: ".min.js"
                    }
                ]
            },
			myTaskModule: {
                options: {
                    // �]�w���Y�����Y�n���J������
                    banner: '/*! <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */\n',
                    // �ϥ� SourceMap �åB�N JS Source �P Map �ɮש�b�@�_
                    sourceMap: true,
                    sourceMapIncludeSources: true
                },
                files: [
                    {
                        expand: true, 
						//cwd: 'js',						
                        // �N���O .min.js ���ɮץ����i�����Y
                        src: ['**/*.src.js', '!*.min.js','!js/**','!bower_components/**','!node_modules/**'],
                        dest: '',
						ext: ".min.js"
                    }
                ]
			},
			//myTaskEmpty: {
			//    options: {
			//        // �]�w���Y�����Y�n���J������
			//        banner: '/*! <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */\n',
			//        // �ϥ� SourceMap �åB�N JS Source �P Map �ɮש�b�@�_
			//        sourceMap: true,
			//        sourceMapIncludeSources: true
			//    },
			//    files: [
            //        {
            //            expand: true,
            //            //cwd: 'js',						
            //            // �N���O .min.js ���ɮץ����i�����Y
            //            src: ['!**/**'],
            //            dest: '',
            //            ext: ".min.js"
            //        }
			//    ]
			//},
			
        },
        // �]�w CSS ���Y task
        cssmin: {
            minify: {
                expand: true,
                cwd: 'css',
                src: ['**/*.css', '!*.min.css'],
                dest: 'css',
				ext: ".min.css"
            }
        },
        jshint: {
            all: [
                '**/*.src.js', '!js/ang/hotkeys.src.js'
            ],
            options: {
                globals: {
                    $: false,
                    jQuery: false
                },
                browser: true,            // browser environment
                devel: true                // 
            }
        },
		watch: {
		   scripts: {
		    files: '**/*.src.js',
		    tasks: ['uglify'],
		    options: {
		        //livereload: true,
		        nospawn: true 
		    },
		   },
		   //js: {
		   //  files: '**/*.src.js',
		   //  tasks: ['uglify:myTaskEmpty'],
		   //  options: {
		   //    //livereload: true,
		   //  },
		   //},
		   css: {
			files: '**/*.css',
			tasks: ['cssmin'],
			options: {
			    //livereload: true,
			    nospawn: true
			},
		   },
		}
    });
 
    // Load the plugin.
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-contrib-watch');
 

    // On watch events, inject only the changed files into the config
    //grunt.event.on('watch', function (action, filepath) {
    //    //change the source and destination in the uglify task at run time so that it affects the changed file only
    //    var destFilePath = filepath.replace(/(.+)\.src.js$/, '$1.min.js');
    //    grunt.config('uglify.myTaskModule.src', filepath);
    //    grunt.config('uglify.myTaskModule.dest', destFilePath);
    //});

    // Default task(s).
    grunt.registerTask('default', ['uglify', 'cssmin', 'watch']);

  
    
};