export interface LoginResponseDto {
    message: string;
    isAuthenticated: boolean;
    token: string;
    username: string;
    
    role: string;
      /* public string Message { get; set; } = null!;
 public bool IsAuthenticated { get; set; }
 public string? Token { get; set; }
 public string? Username { get; set; }
 public string role { get; set; } */
}
