using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HoTroKhachHang.Data.Entities;
using HoTroKhachHang.Application.MHoiDap;
using HoTroKhachHang.Application.MKhachHang;
using HoTroKhachHang.Application.MLoaiHoiDap;
using HoTroKhachHang.Application.MLoaiTaiLieu;
using HoTroKhachHang.Application.MNhanVien;
using HoTroKhachHang.Application.MQuangCao;
using HoTroKhachHang.Application.MTaiLieu;
using HoTroKhachHang.Application.MSanPham;
using HoTroKhachHang.Application.MIdentity;
using HoTroKhachHang.Application.fRepository.Models;
using HoTroKhachHang.Application.MKhachHang.DangNhap;
using HoTroKhachHang.Application.MThongBao;
using HoTroKhachHang.Application.MThongTin;
using HoTroKhachHang.Application.MBanner;
using HoTroKhachHang.Application.MForum;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HoTroKhachHang.Email;
using System.Text.Json.Serialization;
using HoTroKhachHang.Application.MKhachHang.KhachHangDB;
using HoTroKhachHang.Application.MKhachHang.DangKi;
using HoTroKhachHang.Application.MNganhHang;
using HoTroKhachHang.Application.MGiaCa;
using HoTroKhachHang.Application.MNSX;

namespace HoTroKhachHang.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers().AddJsonOptions(x =>
            //        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            var hostingConfig = Configuration
                .GetSection("HostingConfiguration")
                .Get<HostingConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddSingleton(hostingConfig);
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HoTroKhachHang.Api", Version = "v1" });
            });
            services.AddDbContext<CSKHContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DataCSKH")));

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("ServiceConfiguration");
            services.Configure<ServiceConfiguration>(appSettingsSection);
            services.AddTransient<IHoiDapService, HoiDapService>();
            services.AddTransient<ILoaiHoiDapService, LoaiHoiDapService>();
            services.AddTransient<INhanVienService, NhanVienService>();
            services.AddTransient<IQuangCaoService, QuangCaoService>();
            services.AddTransient<ITaiLieuService, TaiLieuService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IDangNhapService, DangNhapService>();
            services.AddTransient<ISanPhamService, SanPhamService>();
            services.AddTransient<ILoaiTaiLieuService, LoaiTaiLieuService>();
            services.AddTransient<IThongBaoService, ThongBaoService>();
            services.AddTransient<IThongTinService, ThongTinService>();
            services.AddTransient<IBannerService, BannerService>();
            services.AddTransient<IForumService, ForumService>();
            services.AddTransient<IKhachHangDBService, KhachHangDBService>();
            services.AddTransient<IDangKiService, DangKiService>();
            services.AddTransient<INganhHangService, NganhHangService>();
            services.AddTransient<IGiaCaService, GiaCaService>();
            services.AddTransient<INSXService, NSXService>();


            // configure jwt authentication
            var serviceConfiguration = appSettingsSection.Get<ServiceConfiguration>();
            var JwtSecretkey = Encoding.ASCII.GetBytes(serviceConfiguration.JwtSettings.Secret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(JwtSecretkey),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;

            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HoTroKhachHang.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();
            
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
