using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace di.proyecto.clase._2025.Backend.Modelos;

public partial class DiinventarioexamenContext : DbContext
{
    public DiinventarioexamenContext()
    {
    }

    public DiinventarioexamenContext(DbContextOptions<DiinventarioexamenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Espacio> Espacios { get; set; }

    public virtual DbSet<Ficheromodelo> Ficheromodelos { get; set; }

    public virtual DbSet<Ficherousuario> Ficherousuarios { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<Modeloarticulo> Modeloarticulos { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Permisosrol> Permisosrols { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Salidum> Salida { get; set; }

    public virtual DbSet<Tipoarticulo> Tipoarticulos { get; set; }

    public virtual DbSet<Tipousuario> Tipousuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().UseMySQL("server=127.0.0.1;port=3306;database=diinventarioexamen;user=root;password=mysql");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.Idarticulo).HasName("PRIMARY");

            entity.Property(e => e.Dentrode).HasComment("Indica que este artículo forma parte de otro");
            entity.Property(e => e.Departamento).HasComment("departamento al que pertenece o del que depende");
            entity.Property(e => e.Espacio).HasComment("espacio en que se encuentra");
            entity.Property(e => e.Fechaalta).HasComment("fecha en que se introdujo en el sistema");
            entity.Property(e => e.Usuariobaja).HasComment("usuario que lo dio de baja");

            entity.HasOne(d => d.DentrodeNavigation).WithMany(p => p.InverseDentrodeNavigation).HasConstraintName("fk_dentrode_articulo");

            entity.HasOne(d => d.DepartamentoNavigation).WithMany(p => p.Articulos).HasConstraintName("fk_departamentos_articulo");

            entity.HasOne(d => d.EspacioNavigation).WithMany(p => p.Articulos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_espacios_articulo");

            entity.HasOne(d => d.ModeloNavigation).WithMany(p => p.Articulos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_modelos_articulo");

            entity.HasOne(d => d.UsuarioaltaNavigation).WithMany(p => p.ArticuloUsuarioaltaNavigations).HasConstraintName("fk_usuarioalta_articulo");

            entity.HasOne(d => d.UsuariobajaNavigation).WithMany(p => p.ArticuloUsuariobajaNavigations).HasConstraintName("fk_usuariobaja_articulo");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Iddepartamento).HasName("PRIMARY");

            entity.Property(e => e.Iddepartamento).HasComment("Departamentos del instituto");
        });

        modelBuilder.Entity<Espacio>(entity =>
        {
            entity.HasKey(e => e.Idespacio).HasName("PRIMARY");

            entity.Property(e => e.Idespacio).HasComment("Cualquier lugar en el que se puede encontrar un artículo.\nUnos espacios pueden estar dentro de otros: relación jerárquica");

            entity.HasOne(d => d.PadreNavigation).WithMany(p => p.InversePadreNavigation).HasConstraintName("fk_espacios_espacio");
        });

        modelBuilder.Entity<Ficheromodelo>(entity =>
        {
            entity.HasKey(e => e.Idficheromodelo).HasName("PRIMARY");

            entity.Property(e => e.Idficheromodelo).HasComment("Permite asociar ficheros a cada modelo de articulo");
            entity.Property(e => e.Modelo).HasComment("Modelo al que pertenece");
            entity.Property(e => e.Nombre).HasComment("Nombre del fichero");
            entity.Property(e => e.Tipo).HasComment("tipo de informacion que contiene");

            entity.HasOne(d => d.ModeloNavigation).WithMany(p => p.Ficheromodelos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_modelos_ficheromodelo");
        });

        modelBuilder.Entity<Ficherousuario>(entity =>
        {
            entity.HasKey(e => e.Idficherousuario).HasName("PRIMARY");

            entity.Property(e => e.Nombre).HasComment("nombre del fichero");
            entity.Property(e => e.Tipo).HasComment("tipo de informacion que contiene");
            entity.Property(e => e.Usuario).HasComment("usuario al que pertenece el fichero");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Ficherousuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuarios_ficherousuario");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.Idgrupo).HasName("PRIMARY");

            entity.Property(e => e.Idgrupo).HasComment("grupos de clase");
        });

        modelBuilder.Entity<Modeloarticulo>(entity =>
        {
            entity.HasKey(e => e.Idmodeloarticulo).HasName("PRIMARY");

            entity.Property(e => e.Idmodeloarticulo).HasComment("Es un catalogo de articulos existentes. De cada modelo puede haber varias unidades con distintos numeros de serie, etc");

            entity.HasOne(d => d.TipoNavigation).WithMany(p => p.Modeloarticulos).HasConstraintName("fk_tipoarticulos_modeloarticulo");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.Idpermiso).HasName("PRIMARY");

            entity.Property(e => e.Idpermiso).HasComment("Las distintas acciones que se pueden realizar sobre las entidades que maneja la aplicacion\n");
            entity.Property(e => e.Permisopadre).HasComment("Permite jerarquizar los permisos de manera que unos dependand de otros.\n\nAqui se indica el id del permiso del que depende o nul si es independiente\n\nPor ejemplo \"alta de usuario\" y \"baja de usuario\" pueden depender de \"acceso a usuarios\"");

            entity.HasOne(d => d.PermisopadreNavigation).WithMany(p => p.InversePermisopadreNavigation).HasConstraintName("fk_permisos_padre");
        });

        modelBuilder.Entity<Permisosrol>(entity =>
        {
            entity.HasKey(e => e.Idpermisosrol).HasName("PRIMARY");

            entity.Property(e => e.Idpermisosrol).HasComment("Permisos asignados a cada rol");
            entity.Property(e => e.Acceso)
                .HasDefaultValueSql("'0'")
                .HasComment("Indica si el permiso se permite, deniega o hereda. En caso de heredarse, se hereda del padre inmediato.\n0: denegado\n1: permitido\n2: heredado");

            entity.HasOne(d => d.PermisoNavigation).WithMany(p => p.Permisosrols)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_permisos_permisosrol");

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.Permisosrols)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_roles_permisosrol");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Idrol).HasName("PRIMARY");

            entity.Property(e => e.Idrol).HasComment("Roles que juegan los usuarios de la aplicacion");
        });

        modelBuilder.Entity<Salidum>(entity =>
        {
            entity.HasKey(e => e.Idsalida).HasName("PRIMARY");

            entity.HasOne(d => d.ArticuloNavigation).WithMany(p => p.Salida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_articulos_salida");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Salida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuarios_salida");
        });

        modelBuilder.Entity<Tipoarticulo>(entity =>
        {
            entity.HasKey(e => e.Idtipoarticulo).HasName("PRIMARY");

            entity.Property(e => e.Idtipoarticulo).HasComment("tipos de articulo: relacion jerárquica\\nMobiliario\\n- Mesa\\n -- Mesa despacho\\n...");
            entity.Property(e => e.Padre).HasComment("tipo de articulo del que depende: relacion jerarquica");

            entity.HasOne(d => d.PadreNavigation).WithMany(p => p.InversePadreNavigation).HasConstraintName("fk_padre_tipoarticulo");
        });

        modelBuilder.Entity<Tipousuario>(entity =>
        {
            entity.HasKey(e => e.Idtipousuario).HasName("PRIMARY");

            entity.Property(e => e.Idtipousuario).HasComment("Para diferenciar tipos de usuario, independientemente del rol que juegan, y poder hacer operaciones masivas con ellos: alumnos, profesores, pas, ...");
            entity.Property(e => e.Nombre).HasComment("descripción del tipo de usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PRIMARY");

            entity.Property(e => e.Username).HasComment("Usuarios de la aplicacion\n");

            entity.HasOne(d => d.DepartamentoNavigation).WithMany(p => p.Usuarios).HasConstraintName("fk_departamentos_usuario");

            entity.HasOne(d => d.GrupoNavigation).WithMany(p => p.Usuarios).HasConstraintName("fk_grupos_usuario");

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.Usuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_roles_usuario");

            entity.HasOne(d => d.TipoNavigation).WithMany(p => p.Usuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tipos_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
