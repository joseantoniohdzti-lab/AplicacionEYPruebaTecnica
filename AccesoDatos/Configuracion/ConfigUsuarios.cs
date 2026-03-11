using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Configuracion
{
    public class ConfigUsuarios : IEntityTypeConfiguration<Usuarios>
    {
        public void Configure(EntityTypeBuilder<Usuarios> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NombreCompleto)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.UserName)
                .HasMaxLength (20) 
                .IsRequired();

            builder.Property(x => x.Password)
                .HasMaxLength (250)
                .IsRequired();

            builder.Property(x => x.Correo)
                .HasMaxLength(512)
                .IsRequired();

            builder.Property(x => x.Estatus)
                .IsRequired();

            builder.Property(x => x.FechaAlta)
                .IsRequired();

            builder.Property(x=> x.FehcaModificacion)
                .IsRequired(false);
        }
    }
}
