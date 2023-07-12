/******************************************************
                  DirectShow .NET
		         brian.low@shaw.ca
*******************************************************/
//					DsVmr9.cs
// Video Mixer Renderer 9, ported from Vmr9.idl


using System;
using System.Runtime.InteropServices;

namespace DShowNET
{

	[ComVisible(false)]
	public enum VMRMode9 : uint
	{
		Windowed                         = 0x00000001,
		Windowless                       = 0x00000002,
		Renderless                       = 0x00000004,
	}

	[ComVisible(false)]
	public enum VMR9AspectRatioMode : uint
	{
		None,
		LetterBox,
	}

	[ComVisible(true), ComImport,
	Guid("5a804648-4f66-4867-9c43-4f5c822cf1b8"),
	InterfaceType( ComInterfaceType.InterfaceIsIUnknown )]
	public interface IVMRFilterConfig9
	{
		[PreserveSig]
		int SetImageCompositor( [In] IntPtr lpVMRImgCompositor );

		[PreserveSig]
		int SetNumberOfStreams( [In] uint dwMaxStreams );

		[PreserveSig]
		int GetNumberOfStreams( [Out] out uint pdwMaxStreams );

		[PreserveSig]
		int SetRenderingPrefs( [In] uint dwRenderFlags );

		[PreserveSig]
		int GetRenderingPrefs( [Out] out uint pdwRenderFlags );

		[PreserveSig]
		int SetRenderingMode( [In] VMRMode9 Mode );

		[PreserveSig]
		int GetRenderingMode( [Out] out VMRMode9 Mode );
	}

	[ComVisible(true), ComImport,
	Guid("8f537d09-f85e-4414-b23b-502e54c79927"),
	InterfaceType( ComInterfaceType.InterfaceIsIUnknown )]
	public interface IVMRWindowlessControl9
	{
		//
		//////////////////////////////////////////////////////////
		// Video size and position information
		//////////////////////////////////////////////////////////
		//
		int GetNativeVideoSize(
			[Out] out int		lpWidth,
			[Out] out int		lpHeight,
			[Out] out int		lpARWidth,
			[Out] out int		lpARHeight
			);

		int GetMinIdealVideoSize(
			[Out] out int		lpHeight
			);

		int GetMaxIdealVideoSize(
			[Out] out int		lpWidth,
			[Out] out int		lpHeight
			);

		int SetVideoPosition(
			[In, MarshalAs(UnmanagedType.LPStruct)] RECT lpSRCRect,
			[In, MarshalAs(UnmanagedType.LPStruct)] RECT lpDSTRect
			);

		int GetVideoPosition(
			[Out, MarshalAs(UnmanagedType.LPStruct)] out RECT lpSRCRect,
			[Out, MarshalAs(UnmanagedType.LPStruct)] out RECT lpDSTRect
			);

		int GetAspectRatioMode( [Out] out VMR9AspectRatioMode lpAspectRatioMode );

		int SetAspectRatioMode( [In] VMR9AspectRatioMode AspectRatioMode );

		//
		//////////////////////////////////////////////////////////
		// Display and clipping management
		//////////////////////////////////////////////////////////
		//
		int SetVideoClippingWindow( [In] IntPtr	hwnd );

		int RepaintVideo(
			[In] IntPtr			hwnd,
			[In] IntPtr			hdc
			);

		int DisplayModeChanged();


		//
		//////////////////////////////////////////////////////////
		// GetCurrentImage
		//
		// Returns the current image being displayed.  This images
		// is returned in the form of packed Windows DIB.
		//
		// GetCurrentImage can be called at any time, also
		// the caller is responsible for free the returned memory
		// by calling CoTaskMemFree.
		//
		// Excessive use of this function will degrade video
		// playback performed.
		//////////////////////////////////////////////////////////
		//
		int GetCurrentImage( [Out] out IntPtr lpDib );

		//
		//////////////////////////////////////////////////////////
		// Border Color control
		//
		// The border color is color used to fill any area of the
		// the destination rectangle that does not contain video.
		// It is typically used in two instances.  When the video
		// straddles two monitors and when the VMR is trying
		// to maintain the aspect ratio of the movies by letter
		// boxing the video to fit within the specified destination
		// rectangle. See SetAspectRatioMode above.
		//////////////////////////////////////////////////////////
		//
		int SetBorderColor( [In] uint Clr );

		int GetBorderColor( [Out] out uint lpClr );

	}

}
