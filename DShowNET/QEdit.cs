/******************************************************
                  DirectShow .NET
		      netmaster@swissonline.ch
*******************************************************/
//					QEdit
// Extended streaming interfaces, ported from qedit.idl

using System;
using System.Runtime.InteropServices;

namespace DShowNET
{



	[ComVisible(true), ComImport,
	Guid("6B652FFF-11FE-4fce-92AD-0266B5D7C78F"),
	InterfaceType( ComInterfaceType.InterfaceIsIUnknown )]
public interface ISampleGrabber
{
		[PreserveSig]
	int SetOneShot(
		[In, MarshalAs(UnmanagedType.Bool)]				bool	OneShot );

		[PreserveSig]
	int SetMediaType(
		[In, MarshalAs(UnmanagedType.LPStruct)]			AMMediaType	pmt );

		[PreserveSig]
	int GetConnectedMediaType(
		[Out, MarshalAs(UnmanagedType.LPStruct)]		AMMediaType	pmt );

		[PreserveSig]
	int SetBufferSamples(
		[In, MarshalAs(UnmanagedType.Bool)]				bool	BufferThem );

		[PreserveSig]
	int GetCurrentBuffer( ref int pBufferSize, IntPtr pBuffer );

		[PreserveSig]
	int GetCurrentSample( IntPtr ppSample );

		[PreserveSig]
	int SetCallback( ISampleGrabberCB pCallback, int WhichMethodToCallback );
}



	[ComVisible(true), ComImport,
	Guid("0579154A-2B53-4994-B0D0-E773148EFF85"),
	InterfaceType( ComInterfaceType.InterfaceIsIUnknown )]
public interface ISampleGrabberCB
{
		[PreserveSig]
	int SampleCB( double SampleTime, IMediaSample pSample );

		[PreserveSig]
	int BufferCB( double SampleTime, IntPtr pBuffer, int BufferLen );
}



	[StructLayout(LayoutKind.Sequential), ComVisible(false)]
public class VideoInfoHeader		// VIDEOINFOHEADER
{
	public DsRECT	SrcRect;
	public DsRECT	TargetRect;
	public int		BitRate;
	public int		BitErrorRate;
	public long		AvgTimePerFrame;
	public BitmapInfoHeader	BmiHeader;
}

	[StructLayout(LayoutKind.Sequential), ComVisible(false)]
	public class VideoInfoHeader2		// VIDEOINFOHEADER2
	{
		public DsRECT			SrcRect;
		public DsRECT			TargetRect;
		public int				BitRate;
		public int				BitErrorRate;
		public long				AvgTimePerFrame;
		public int				InterlaceFlags;
		public int				CopyProtectFlags;
		public int				PictAspectRatioX; 
		public int				PictAspectRatioY; 
		public int				ControlFlags;
		public int              Reserved2;
		public BitmapInfoHeader	BmiHeader;
	};


	[StructLayout(LayoutKind.Sequential), ComVisible(false)]
	public class WaveFormatEx
	{
		public short wFormatTag;
		public short nChannels;
		public int nSamplesPerSec;
		public int nAvgBytesPerSec;
		public short nBlockAlign;
		public short wBitsPerSample;
		public short cbSize;
	}

} // namespace DShowNET
